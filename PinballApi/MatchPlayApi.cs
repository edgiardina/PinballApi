using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using PinballApi.Http;
using PinballApi.Interfaces;
using PinballApi.Models.MatchPlay;
using PinballApi.Models.MatchPlay.Opdb;
using PinballApi.Models.MatchPlay.SeriesStats;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi
{
    /// <summary>
    /// The read-only MatchPlay Events API. It also serves the OPDB machine database and PinTips.
    /// </summary>
    /// <remarks>
    /// Get an API token at https://app.matchplay.events/account/tokens. MatchPlay rate limits most
    /// endpoints to 120 requests per minute and some, such as search and the tournament summaries,
    /// to 6. Cache what you fetch, and prefer the bulk exports over a loop of single-entry calls.
    /// Every call raises <see cref="PinballApiException"/> when the service refuses it.
    /// </remarks>
    public class MatchPlayApi : IMatchPlayApi
    {
        private const string BaseUrl = "https://app.matchplay.events/api/";

        protected readonly string ApiToken;

        private readonly IFlurlClient client;

        protected readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        protected IFlurlRequest BaseRequest => client.Request()
                                                    .WithOAuthBearerToken(ApiToken)
                                                    .WithHeader("Content-Type", "application/json")
                                                    .WithHeader("Accept", "application/json");

        /// <param name="apiToken">A token from https://app.matchplay.events/account/tokens.</param>
        /// <param name="rateLimitRetryCount">
        /// How many times to wait and try again when MatchPlay answers HTTP 429. The default of
        /// zero raises the error to the caller at once. Set it to 1 or more to let the client wait
        /// out the window. A wait can last a full minute, so leave it at zero when the caller
        /// cannot afford to block.
        /// </param>
        public MatchPlayApi(string apiToken, int rateLimitRetryCount = 0)
        {
            this.ApiToken = apiToken;

            var builder = new FlurlClientBuilder(BaseUrl);

            if (rateLimitRetryCount > 0)
            {
                builder.AddMiddleware(() => new RateLimitRetryHandler(rateLimitRetryCount));
            }

            client = builder.Build();
            client.WithSettings(settings =>
            {
                settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions);
            });
        }

        #region request plumbing

        /// <summary>
        /// Send a request whose body wraps the payload under a <c>data</c> root key.
        /// </summary>
        private async Task<T> GetData<T>(IFlurlRequest request, CancellationToken cancellationToken)
        {
            var json = await GetString(request, cancellationToken).ConfigureAwait(false);

            var data = JsonNode.Parse(json)?["data"];

            return data == null ? default : data.Deserialize<T>(JsonSerializerOptions);
        }

        /// <summary>
        /// Send a request whose body maps straight onto the result type.
        /// </summary>
        private async Task<T> GetJson<T>(IFlurlRequest request, CancellationToken cancellationToken)
        {
            var json = await GetString(request, cancellationToken).ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
        }

        /// <summary>
        /// Send a request and keep the paging links, so a caller can walk to the next page.
        /// </summary>
        private async Task<PagedResult<T>> GetPage<T>(IFlurlRequest request, CancellationToken cancellationToken)
        {
            var json = await GetString(request, cancellationToken).ConfigureAwait(false);

            return JsonSerializer.Deserialize<PagedResult<T>>(json, JsonSerializerOptions);
        }

        private async Task<string> GetString(IFlurlRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await request.GetStringAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                throw await ToApiException(ex, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Turn a Flurl failure into the exception the caller should see.
        /// </summary>
        /// <remarks>
        /// A cancelled call is not an API error. Flurl reports it as a failed call, so unwrap it
        /// back into <see cref="OperationCanceledException"/> and let the caller handle
        /// cancellation the usual way.
        /// </remarks>
        private static async Task<Exception> ToApiException(FlurlHttpException ex, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return new OperationCanceledException(ex.Message, ex, cancellationToken);
            }

            string body = null;

            try
            {
                body = await ex.GetResponseStringAsync().ConfigureAwait(false);
            }
            catch
            {
                // The body is a courtesy. Never let reading it hide the original failure.
            }

            var status = ex.StatusCode.HasValue ? (System.Net.HttpStatusCode?)ex.StatusCode.Value : null;

            return new PinballApiException(ex.Message, status, body, ex.Call?.Request?.Url?.ToString(), ex);
        }

        /// <summary>
        /// Walk every page of a list endpoint. MatchPlay reports the next page in
        /// <c>links.next</c>, and sends null there on the last page.
        /// </summary>
        /// <param name="requestForPage">Builds the request for a given page number.</param>
        private async IAsyncEnumerable<T> Enumerate<T>(Func<int, IFlurlRequest> requestForPage,
                                                       [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var page = 1;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await GetPage<T>(requestForPage(page), cancellationToken).ConfigureAwait(false);

                if (result?.Data == null || result.Data.Count == 0)
                {
                    yield break;
                }

                foreach (var item in result.Data)
                {
                    yield return item;
                }

                if (!result.HasMore)
                {
                    yield break;
                }

                page++;
            }
        }

        /// <summary>
        /// Add an include flag only when it is true. MatchPlay reads these flags by presence, so
        /// sending the value "false" still turns the include on.
        /// </summary>
        private static IFlurlRequest SetIncludeFlag(IFlurlRequest request, string name, bool value)
        {
            return value ? request.SetQueryParam(name, 1) : request;
        }

        private static string JoinResolveIds(List<int> ids, string parameterName)
        {
            return JoinCappedIds(ids, MaxResolveIds, parameterName);
        }

        private static string JoinCappedIds(List<int> ids, int max, string parameterName)
        {
            if (ids == null || ids.Count == 0)
            {
                throw new ArgumentException("Provide at least one id.", parameterName);
            }

            if (ids.Count > max)
            {
                throw new ArgumentException($"Provide no more than {max} ids.", parameterName);
            }

            return string.Join(",", ids);
        }

        #endregion

        #region players, arenas & profiles

        private IFlurlRequest ArenasRequest(Status status, List<int> arenaIds, int page)
        {
            var request = BaseRequest
                .AppendPathSegment("arenas")
                .SetQueryParam("status", status.ToString().ToLower())
                .SetQueryParam("page", page);

            if (arenaIds != null && arenaIds.Any())
            {
                request = request.SetQueryParam("arenas", string.Join(",", arenaIds));
            }

            return request;
        }

        /// <summary>
        /// Get the arenas that belong to the organizer who owns the API token.
        /// </summary>
        public async Task<List<Arena>> GetArenas(Status status = Status.Active, List<int> arenaIds = null, int page = 1,
                                                 CancellationToken cancellationToken = default)
        {
            return await GetData<List<Arena>>(ArenasRequest(status, arenaIds, page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetArenas"/>.
        /// </summary>
        public IAsyncEnumerable<Arena> EnumerateArenas(Status status = Status.Active, List<int> arenaIds = null,
                                                       CancellationToken cancellationToken = default)
        {
            return Enumerate<Arena>(page => ArenasRequest(status, arenaIds, page), cancellationToken);
        }

        private IFlurlRequest LocationsRequest(Status? status, List<int> locationIds, int page)
        {
            var request = BaseRequest
                .AppendPathSegment("locations")
                .SetQueryParam("page", page);

            if (locationIds != null && locationIds.Any())
            {
                request = request.SetQueryParam("locations", string.Join(",", locationIds));
            }

            if (status.HasValue)
            {
                request = request.SetQueryParam("status", status.Value.ToString().ToLower());
            }

            return request;
        }

        public async Task<List<Location>> GetLocations(Status? status = null, List<int> locationIds = null, int page = 1,
                                                       CancellationToken cancellationToken = default)
        {
            return await GetData<List<Location>>(LocationsRequest(status, locationIds, page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetLocations"/>.
        /// </summary>
        public IAsyncEnumerable<Location> EnumerateLocations(Status? status = null, List<int> locationIds = null,
                                                             CancellationToken cancellationToken = default)
        {
            return Enumerate<Location>(page => LocationsRequest(status, locationIds, page), cancellationToken);
        }

        private IFlurlRequest PlayersRequest(Status? status, List<int> players, int page)
        {
            var request = BaseRequest
                .AppendPathSegment("players")
                .SetQueryParam("page", page);

            if (players != null && players.Any())
            {
                request = request.SetQueryParam("players", string.Join(",", players));
            }

            if (status.HasValue)
            {
                request = request.SetQueryParam("status", status.Value.ToString().ToLower());
            }

            return request;
        }

        /// <summary>
        /// Get the players that belong to the organizer who owns the API token.
        /// </summary>
        public async Task<List<Player>> GetPlayers(Status? status = null, List<int> players = null, int page = 1,
                                                   CancellationToken cancellationToken = default)
        {
            return await GetData<List<Player>>(PlayersRequest(status, players, page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetPlayers"/>.
        /// </summary>
        public IAsyncEnumerable<Player> EnumeratePlayers(Status? status = null, List<int> players = null,
                                                         CancellationToken cancellationToken = default)
        {
            return Enumerate<Player>(page => PlayersRequest(status, players, page), cancellationToken);
        }

        public async Task<User> GetMyProfile(CancellationToken cancellationToken = default)
        {
            return await GetData<User>(BaseRequest
                            .AppendPathSegment("users")
                            .AppendPathSegment("profile"), cancellationToken);
        }

        public async Task<UserProfile> GetProfile(int playerId, CancellationToken cancellationToken = default)
        {
            return await GetJson<UserProfile>(BaseRequest
                            .AppendPathSegment("users")
                            .AppendPathSegment(playerId)
                            .SetQueryParam("includeIfpa", "true")
                            .SetQueryParam("includeCounts", "true"), cancellationToken);
        }

        public async Task<List<User>> SearchForUsers(string searchText, CancellationToken cancellationToken = default)
        {
            return await GetData<List<User>>(BaseRequest
                .AppendPathSegment("search")
                .SetQueryParam("query", searchText)
                .SetQueryParam("type", "users"), cancellationToken);
        }

        public async Task<List<Tournament>> SearchForTournaments(string searchText, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Tournament>>(BaseRequest
                .AppendPathSegment("search")
                .SetQueryParam("query", searchText)
                .SetQueryParam("type", "tournaments"), cancellationToken);
        }

        #endregion

        #region resolve unknown

        /// <summary>
        /// The largest number of ids the resolve-unknown endpoints accept in one call.
        /// </summary>
        public const int MaxResolveIds = 25;

        /// <summary>
        /// Get the player objects for a set of player ids.
        /// </summary>
        /// <remarks>
        /// Most MatchPlay responses carry only a player id. Use this to fill in the rest. Ask for
        /// the tournament players with <c>includePlayers</c> on <see cref="GetTournament"/> first,
        /// and use this only for the ids that are left.
        /// </remarks>
        /// <param name="playerIds">Up to 25 player ids.</param>
        public async Task<List<Player>> ResolveUnknownPlayers(List<int> playerIds, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Player>>(BaseRequest
                .AppendPathSegment("players")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("players", JoinResolveIds(playerIds, nameof(playerIds))), cancellationToken);
        }

        /// <summary>
        /// Get the arena objects for a set of arena ids.
        /// </summary>
        /// <param name="arenaIds">Up to 25 arena ids.</param>
        public async Task<List<Arena>> ResolveUnknownArenas(List<int> arenaIds, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Arena>>(BaseRequest
                .AppendPathSegment("arenas")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("arenas", JoinResolveIds(arenaIds, nameof(arenaIds))), cancellationToken);
        }

        /// <summary>
        /// Get the profile objects for a set of user ids.
        /// </summary>
        /// <param name="userIds">Up to 25 user ids.</param>
        public async Task<List<User>> ResolveUnknownUsers(List<int> userIds, CancellationToken cancellationToken = default)
        {
            return await GetData<List<User>>(BaseRequest
                .AppendPathSegment("users")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("users", JoinResolveIds(userIds, nameof(userIds))), cancellationToken);
        }

        /// <summary>
        /// Get the player objects for a set of player ids, with the tournament pivot data.
        /// </summary>
        /// <remarks>
        /// The result fills in <see cref="Player.TournamentPlayer"/> with the label, the seed and
        /// the active status for this tournament.
        /// </remarks>
        /// <param name="tournamentId">The tournament the players belong to.</param>
        /// <param name="playerIds">Up to 25 player ids.</param>
        public async Task<List<Player>> ResolveUnknownTournamentPlayers(int tournamentId, List<int> playerIds,
                                                                        CancellationToken cancellationToken = default)
        {
            return await GetData<List<Player>>(BaseRequest
                .AppendPathSegment("tournaments")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("players")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("players", JoinResolveIds(playerIds, nameof(playerIds))), cancellationToken);
        }

        /// <summary>
        /// Get the arena objects for a set of arena ids, with the tournament pivot data.
        /// </summary>
        /// <remarks>
        /// The result fills in <see cref="Arena.TournamentArena"/> with the label and the active
        /// status for this tournament.
        /// </remarks>
        /// <param name="tournamentId">The tournament the arenas belong to.</param>
        /// <param name="arenaIds">Up to 25 arena ids.</param>
        public async Task<List<Arena>> ResolveUnknownTournamentArenas(int tournamentId, List<int> arenaIds,
                                                                      CancellationToken cancellationToken = default)
        {
            return await GetData<List<Arena>>(BaseRequest
                .AppendPathSegment("tournaments")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("arenas")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("arenas", JoinResolveIds(arenaIds, nameof(arenaIds))), cancellationToken);
        }

        #endregion

        #region series

        private IFlurlRequest SeriesListRequest(int? ownerUserId, int? playedUserId, SeriesStatus? seriesStatus, int page)
        {
            var request = BaseRequest
                            .AppendPathSegment("series")
                            .SetQueryParam("page", page);

            if (seriesStatus.HasValue)
            {
                request = request.SetQueryParam("status", seriesStatus.Value.ToString().ToLower());
            }

            if (ownerUserId.HasValue)
            {
                request = request.SetQueryParam("owner", ownerUserId);
            }

            if (playedUserId.HasValue)
            {
                request = request.SetQueryParam("played", playedUserId);
            }

            return request;
        }

        /// <summary>
        /// Get a list of series.
        /// </summary>
        public async Task<List<Series>> GetSeriesList(int? ownerUserId = null, int? playedUserId = null, SeriesStatus? seriesStatus = null,
                                                      int page = 1, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Series>>(SeriesListRequest(ownerUserId, playedUserId, seriesStatus, page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetSeriesList"/>.
        /// </summary>
        public IAsyncEnumerable<Series> EnumerateSeries(int? ownerUserId = null, int? playedUserId = null, SeriesStatus? seriesStatus = null,
                                                        CancellationToken cancellationToken = default)
        {
            return Enumerate<Series>(page => SeriesListRequest(ownerUserId, playedUserId, seriesStatus, page), cancellationToken);
        }

        /// <summary>
        /// Get one series.
        /// </summary>
        public async Task<Series> GetSeries(int seriesId, CancellationToken cancellationToken = default)
        {
            return await GetData<Series>(BaseRequest
                           .AppendPathSegment("series")
                           .AppendPathSegment(seriesId)
                           .SetQueryParam("includeDetails", true), cancellationToken);
        }

        public async Task<List<Player>> GetSeriesAttendance(int seriesId, int count, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Player>>(BaseRequest
                           .AppendPathSegment("series")
                           .AppendPathSegment(seriesId)
                           .AppendPathSegment("stats")
                           .AppendPathSegment("attendance")
                           .SetQueryParam("count", count), cancellationToken);
        }

        public async Task<SeriesStats> GetSeriesStats(int seriesId, CancellationToken cancellationToken = default)
        {
            return await GetJson<SeriesStats>(BaseRequest
                            .AppendPathSegment("series")
                            .AppendPathSegment(seriesId)
                            .AppendPathSegment("stats"), cancellationToken);
        }

        #endregion

        #region ratings

        public async Task<RatingComparison> ComparePlayers(List<int> playerIds = null, List<int> ifpaIds = null, List<int> userIds = null,
                                                           CancellationToken cancellationToken = default)
        {
            GuardComparisonCount(playerIds, nameof(playerIds));
            GuardComparisonCount(ifpaIds, nameof(ifpaIds));
            GuardComparisonCount(userIds, nameof(userIds));

            try
            {
                return await BaseRequest
                    .AppendPathSegment("ratings/compare")
                    .PostJsonAsync(new { ifpaIds, playerIds, userIds }, cancellationToken: cancellationToken)
                    .ReceiveJson<RatingComparison>();
            }
            catch (FlurlHttpException ex)
            {
                throw await ToApiException(ex, cancellationToken);
            }
        }

        private static void GuardComparisonCount(List<int> ids, string parameterName)
        {
            if (ids != null && ids.Count > 24)
            {
                throw new ArgumentException($"{parameterName} cannot have more than 24 items", parameterName);
            }
        }

        public async Task<RatingProfile> GetRatingProfile(int id, RatingQueryType ratingQueryType, CancellationToken cancellationToken = default)
        {
            return await GetJson<RatingProfile>(BaseRequest
                .AppendPathSegment("ratings")
                .AppendPathSegment(ratingQueryType.ToString().ToLower())
                .AppendPathSegment(id), cancellationToken);
        }

        public async Task<List<Rating>> GetCurrentRatingData(List<int> ifpaIds = null, List<int> userIds = null, int page = 1,
                                                             CancellationToken cancellationToken = default)
        {
            GuardComparisonCount(ifpaIds, nameof(ifpaIds));
            GuardComparisonCount(userIds, nameof(userIds));

            var data = new GetCurrentRatingDataPayload(ifpaIds, userIds);

            try
            {
                var json = await BaseRequest
                    .AppendPathSegment("ratings")
                    .SetQueryParam("page", page)
                    .SendJsonAsync(HttpMethod.Get, data, cancellationToken: cancellationToken)
                    .ReceiveString();

                return JsonNode.Parse(json)?["data"]?.Deserialize<List<Rating>>(JsonSerializerOptions);
            }
            catch (FlurlHttpException ex)
            {
                throw await ToApiException(ex, cancellationToken);
            }
        }

        public async Task<List<RatingPeriod>> GetRatingPeriods(int page = 1, CancellationToken cancellationToken = default)
        {
            return await GetData<List<RatingPeriod>>(BaseRequest
               .AppendPathSegment("rating-periods")
               .SetQueryParam("page", page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetRatingPeriods"/>.
        /// </summary>
        public IAsyncEnumerable<RatingPeriod> EnumerateRatingPeriods(CancellationToken cancellationToken = default)
        {
            return Enumerate<RatingPeriod>(page => BaseRequest
               .AppendPathSegment("rating-periods")
               .SetQueryParam("page", page), cancellationToken);
        }

        public async Task<List<IfpaRatingHistory>> GetRatingHistoryByIfpaId(int ifpaId, int limit = 100, int page = 1,
                                                                            CancellationToken cancellationToken = default)
        {
            return await GetData<List<IfpaRatingHistory>>(BaseRequest
               .AppendPathSegment("ifpa")
               .AppendPathSegment(ifpaId)
               .AppendPathSegment("rating-history")
               .SetQueryParam("page", page)
               .SetQueryParam("limit", limit), cancellationToken);
        }

        public async Task<SingleRatingPeriod> GetRatingPeriod(DateTime date, CancellationToken cancellationToken = default)
        {
            return await GetJson<SingleRatingPeriod>(BaseRequest
                           .AppendPathSegment("rating-periods")
                           .AppendPathSegment(date.ToString("yyyy-MM-dd")), cancellationToken);
        }

        class GetCurrentRatingDataPayload
        {
            public GetCurrentRatingDataPayload(List<int> ifpaIds, List<int> userIds)
            {
                if (userIds != null)
                    this.userIds = string.Join(",", userIds);

                if (ifpaIds != null)
                    this.ifpaIds = string.Join(",", ifpaIds);
            }
            public readonly string ifpaIds;
            public readonly string userIds;
        }

        #endregion

        #region opdb & pintips

        /// <summary>
        /// The full OPDB data set in the current (v2) format. It matches <see cref="OpdbEntry"/>.
        /// </summary>
        public const string OpdbExportUrl = "https://mp-data.sfo3.cdn.digitaloceanspaces.com/opdb-v2.json";

        /// <summary>
        /// The cut down OPDB data set. It holds the machine name, the manufacturer name and the
        /// backglass image only, and it matches <see cref="OpdbSlimEntry"/>.
        /// </summary>
        public const string OpdbSlimExportUrl = "https://mp-data.sfo3.cdn.digitaloceanspaces.com/opdb-slim.json";

        /// <summary>
        /// The full OPDB data set in the legacy (v1) format. Kept for older consumers only.
        /// This library does not model it. Use <see cref="OpdbExportUrl"/> instead.
        /// </summary>
        public const string OpdbLegacyExportUrl = "https://mp-data.sfo3.cdn.digitaloceanspaces.com/latest-opdb.json";

        /// <summary>
        /// The full PinTips data set. It matches <see cref="PinTip"/>.
        /// </summary>
        public const string PinTipsExportUrl = "https://mp-data.sfo3.cdn.digitaloceanspaces.com/latest-pintips.json";

        /// <summary>
        /// Get a single entry from the Open Pinball Database.
        /// </summary>
        /// <param name="opdbId">The OPDB id of a machine group, a machine or an alias.</param>
        /// <param name="includePeople">Include the people credited on the entry.</param>
        /// <param name="includeImages">Include the images for the entry.</param>
        /// <param name="cancellationToken">Cancels the call.</param>
        /// <remarks>
        /// Do not call this in a loop to build a local catalog. Download <see cref="GetOpdbExport"/>
        /// once instead, and store the result.
        /// </remarks>
        public async Task<OpdbEntry> GetOpdbEntry(string opdbId, bool includePeople = false, bool includeImages = false,
                                                  CancellationToken cancellationToken = default)
        {
            var request = BaseRequest
                .AppendPathSegment("opdb")
                .AppendPathSegment("entry")
                .AppendPathSegment(opdbId);

            request = SetIncludeFlag(request, "includePeople", includePeople);
            request = SetIncludeFlag(request, "includeImages", includeImages);

            return await GetData<OpdbEntry>(request, cancellationToken);
        }

        /// <summary>
        /// Get every OPDB id that was moved or removed. Use it to repair ids you stored earlier.
        /// </summary>
        public async Task<List<OpdbChangelogEntry>> GetOpdbChangelog(CancellationToken cancellationToken = default)
        {
            return await GetData<List<OpdbChangelogEntry>>(BaseRequest
                .AppendPathSegment("opdb")
                .AppendPathSegment("changelog"), cancellationToken);
        }

        /// <summary>
        /// Get the PinTips for one OPDB entry.
        /// </summary>
        /// <param name="opdbId">The OPDB id to get tips for.</param>
        /// <param name="cancellationToken">Cancels the call.</param>
        public async Task<PinTipsResult> GetPinTipsByOpdbId(string opdbId, CancellationToken cancellationToken = default)
        {
            return await GetJson<PinTipsResult>(BaseRequest
                .AppendPathSegment("pintips")
                .SetQueryParam("opdbId", opdbId), cancellationToken);
        }

        /// <summary>
        /// Get the PinTips for the machine behind a Match Play arena.
        /// </summary>
        /// <param name="arenaId">The Match Play arena to get tips for.</param>
        /// <param name="cancellationToken">Cancels the call.</param>
        public async Task<PinTipsResult> GetPinTipsByArenaId(int arenaId, CancellationToken cancellationToken = default)
        {
            return await GetJson<PinTipsResult>(BaseRequest
                .AppendPathSegment("pintips")
                .SetQueryParam("arenaId", arenaId), cancellationToken);
        }

        /// <summary>
        /// Download the full OPDB data set from the Match Play CDN.
        /// </summary>
        /// <remarks>
        /// The download is several megabytes and needs no API token. Fetch it on a schedule,
        /// store the result, and serve searches and typeaheads from your own store.
        /// </remarks>
        public async Task<List<OpdbEntry>> GetOpdbExport(CancellationToken cancellationToken = default)
        {
            var export = await GetExport<OpdbExport>(OpdbExportUrl, cancellationToken);

            return export?.Entries;
        }

        /// <summary>
        /// Download the cut down OPDB data set from the Match Play CDN.
        /// </summary>
        /// <remarks>
        /// Use this when you only need names and backglass images, for example to build a
        /// machine picker. It needs no API token.
        /// </remarks>
        public async Task<List<OpdbSlimEntry>> GetOpdbSlimExport(CancellationToken cancellationToken = default)
        {
            var export = await GetExport<OpdbSlimExport>(OpdbSlimExportUrl, cancellationToken);

            return export?.Entries;
        }

        /// <summary>
        /// Download the full PinTips data set from the Match Play CDN. It needs no API token.
        /// </summary>
        public async Task<List<PinTip>> GetPinTipsExport(CancellationToken cancellationToken = default)
        {
            return await GetExport<List<PinTip>>(PinTipsExportUrl, cancellationToken);
        }

        private async Task<T> GetExport<T>(string url, CancellationToken cancellationToken)
        {
            try
            {
                using (var stream = await url.GetStreamAsync(cancellationToken: cancellationToken).ConfigureAwait(false))
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptions, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (FlurlHttpException ex)
            {
                throw await ToApiException(ex, cancellationToken).ConfigureAwait(false);
            }
        }

        private class OpdbExport
        {
            [JsonPropertyName("entries")]
            public List<OpdbEntry> Entries { get; set; }
        }

        private class OpdbSlimExport
        {
            [JsonPropertyName("entries")]
            public List<OpdbSlimEntry> Entries { get; set; }
        }

        #endregion

        #region tournaments

        private IFlurlRequest TournamentsRequest(int? ownerUserId, int? playedUserId, TournamentStatus? status, int? seriesId, int page, int? limit)
        {
            var request = BaseRequest
                .AppendPathSegment("tournaments")
                .SetQueryParam("page", page);

            if (limit.HasValue)
            {
                request = request.SetQueryParam("limit", limit.Value);
            }

            if (ownerUserId.HasValue)
            {
                request = request.SetQueryParam("owner", ownerUserId);
            }

            if (playedUserId.HasValue)
            {
                request = request.SetQueryParam("played", playedUserId);
            }

            if (status.HasValue)
            {
                request = request.SetQueryParam("status", status.ToString().ToLower());
            }

            if (seriesId.HasValue)
            {
                request = request.SetQueryParam("series", seriesId);
            }

            return request;
        }

        /// <summary>
        /// Get a list of tournaments.
        /// </summary>
        /// <remarks>
        /// Do not page through thousands of tournaments with this. Contact MatchPlay if you need
        /// bulk data.
        /// </remarks>
        /// <param name="limit">Results per page. The API defaults to 25 and allows up to 100.</param>
        public async Task<List<Tournament>> GetTournaments(int? ownerUserId = null, int? playedUserId = null, TournamentStatus? status = null,
                                                           int? seriesId = null, int page = 1, int? limit = null,
                                                           CancellationToken cancellationToken = default)
        {
            return await GetData<List<Tournament>>(TournamentsRequest(ownerUserId, playedUserId, status, seriesId, page, limit), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetTournaments"/>.
        /// </summary>
        /// <remarks>
        /// MatchPlay asks that you do not walk thousands of tournaments. Filter the list first.
        /// </remarks>
        public IAsyncEnumerable<Tournament> EnumerateTournaments(int? ownerUserId = null, int? playedUserId = null, TournamentStatus? status = null,
                                                                 int? seriesId = null, int? limit = null,
                                                                 CancellationToken cancellationToken = default)
        {
            return Enumerate<Tournament>(page => TournamentsRequest(ownerUserId, playedUserId, status, seriesId, page, limit), cancellationToken);
        }

        /// <summary>
        /// Get one tournament, and optionally the related objects.
        /// </summary>
        /// <remarks>
        /// The endpoint reads each include flag by presence, not by value. Sending
        /// <c>includePlayers=false</c> still returns the players. This method therefore sends a
        /// flag only when it is true. Ask for what you need. Each extra flag costs response size.
        /// </remarks>
        public async Task<Tournament> GetTournament(int tournamentId, bool includePlayers = false, bool includeArenas = false, bool includeBanks = false,
                                                    bool includeScorekeepers = false, bool includeSeries = false, bool includeLocation = false,
                                                    bool includeRsvpConfiguration = false, bool includeParent = false, bool includePlayoffs = false,
                                                    bool includeShortcut = false, bool includeEntryConfiguration = false,
                                                    bool includeLinkedTournaments = false, bool includeEvent = false,
                                                    CancellationToken cancellationToken = default)
        {
            var request = BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId);

            request = SetIncludeFlag(request, "includePlayers", includePlayers);
            request = SetIncludeFlag(request, "includeArenas", includeArenas);
            request = SetIncludeFlag(request, "includeBanks", includeBanks);
            request = SetIncludeFlag(request, "includeScorekeepers", includeScorekeepers);
            request = SetIncludeFlag(request, "includeSeries", includeSeries);
            request = SetIncludeFlag(request, "includeLocation", includeLocation);
            request = SetIncludeFlag(request, "includeRsvpConfiguration", includeRsvpConfiguration);
            request = SetIncludeFlag(request, "includeParent", includeParent);
            request = SetIncludeFlag(request, "includePlayoffs", includePlayoffs);
            request = SetIncludeFlag(request, "includeShortcut", includeShortcut);
            request = SetIncludeFlag(request, "includeEntryConfiguration", includeEntryConfiguration);
            request = SetIncludeFlag(request, "includeLinkedTournaments", includeLinkedTournaments);
            request = SetIncludeFlag(request, "includeEvent", includeEvent);

            return await GetData<Tournament>(request, cancellationToken);
        }

        public async Task<List<Standing>> GetStandings(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<List<Standing>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("standings"), cancellationToken);
        }

        public async Task<List<Round>> GetRounds(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Round>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("rounds"), cancellationToken);
        }

        /// <summary>
        /// Get the play queues of a tournament. Flip Frenzy uses these.
        /// </summary>
        /// <remarks>
        /// Returns an empty list for a format that does not queue players. MatchPlay answers 403
        /// unless the token has scorekeeper permission on the tournament.
        /// </remarks>
        public async Task<List<Queue>> GetQueues(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Queue>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("queues"), cancellationToken);
        }

        /// <summary>
        /// Estimate the WPPR value of a tournament or a series.
        /// </summary>
        public async Task<IfpaEstimate> GetIfpaEstimate(int? tournamentId = null, int? seriesId = null, List<int> ifpaIds = null,
                                                        List<string> names = null, CancellationToken cancellationToken = default)
        {
            if (tournamentId.HasValue == false && seriesId.HasValue == false && ifpaIds == null && names == null)
            {
                throw new ArgumentException("Provide EITHER a tournament id OR a series id OR a list of ifpaIds/names.");
            }

            var request = BaseRequest
                .AppendPathSegment("ifpa/wppr-estimator");

            if (tournamentId.HasValue)
            {
                request = request.SetQueryParam("tournamentId", tournamentId.Value);
            }

            if (seriesId.HasValue)
            {
                request = request.SetQueryParam("seriesId", seriesId.Value);
            }

            if (ifpaIds != null && ifpaIds.Any())
            {
                request = request.SetQueryParam("ifpaIds", ifpaIds);
            }

            if (names != null && names.Any())
            {
                request = request.SetQueryParam("names", names);
            }

            try
            {
                var response = await request.PostAsync(cancellationToken: cancellationToken);

                return await response.GetJsonAsync<IfpaEstimate>();
            }
            catch (FlurlHttpException ex)
            {
                throw await ToApiException(ex, cancellationToken);
            }
        }

        private IFlurlRequest SummaryRequest(int tournamentId, string kind, int page)
        {
            return BaseRequest
                .AppendPathSegment("tournaments")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("summary")
                .AppendPathSegment(kind)
                .SetQueryParam("page", page);
        }

        /// <summary>
        /// Get one entry per arena in a tournament, with the number of games played on it.
        /// </summary>
        /// <remarks>
        /// MatchPlay builds this only after the tournament is complete. It returns an empty list
        /// for a tournament that is still open.
        /// </remarks>
        public async Task<List<TournamentArenaSummary>> GetTournamentArenaSummary(int tournamentId, int page = 1,
                                                                                  CancellationToken cancellationToken = default)
        {
            return await GetData<List<TournamentArenaSummary>>(SummaryRequest(tournamentId, "arenas", page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetTournamentArenaSummary"/>.
        /// </summary>
        public IAsyncEnumerable<TournamentArenaSummary> EnumerateTournamentArenaSummary(int tournamentId,
                                                                                        CancellationToken cancellationToken = default)
        {
            return Enumerate<TournamentArenaSummary>(page => SummaryRequest(tournamentId, "arenas", page), cancellationToken);
        }

        /// <summary>
        /// Get one entry per player and arena pair in a tournament, with wins and losses.
        /// </summary>
        /// <remarks>
        /// MatchPlay builds this only after the tournament is complete. It returns an empty list
        /// for a tournament that is still open.
        /// </remarks>
        public async Task<List<TournamentPlayerArenaSummary>> GetTournamentPlayerArenaSummary(int tournamentId, int page = 1,
                                                                                              CancellationToken cancellationToken = default)
        {
            return await GetData<List<TournamentPlayerArenaSummary>>(SummaryRequest(tournamentId, "player-arenas", page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetTournamentPlayerArenaSummary"/>.
        /// </summary>
        public IAsyncEnumerable<TournamentPlayerArenaSummary> EnumerateTournamentPlayerArenaSummary(int tournamentId,
                                                                                                    CancellationToken cancellationToken = default)
        {
            return Enumerate<TournamentPlayerArenaSummary>(page => SummaryRequest(tournamentId, "player-arenas", page), cancellationToken);
        }

        /// <summary>
        /// Get one entry per player, opponent and arena combination in a tournament.
        /// </summary>
        /// <remarks>
        /// MatchPlay builds this only after the tournament is complete. The data is duplicated on
        /// purpose. Each pair of players appears once from each side.
        /// </remarks>
        public async Task<List<TournamentMatchSummary>> GetTournamentMatchSummary(int tournamentId, int page = 1,
                                                                                  CancellationToken cancellationToken = default)
        {
            return await GetData<List<TournamentMatchSummary>>(SummaryRequest(tournamentId, "matches", page), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetTournamentMatchSummary"/>.
        /// </summary>
        public IAsyncEnumerable<TournamentMatchSummary> EnumerateTournamentMatchSummary(int tournamentId,
                                                                                        CancellationToken cancellationToken = default)
        {
            return Enumerate<TournamentMatchSummary>(page => SummaryRequest(tournamentId, "matches", page), cancellationToken);
        }

        #endregion

        #region games

        private IFlurlRequest GamesRequest(List<int> tournamentIds, int? playerId, int? arenaId, int? round, int? bank,
                                           GameStatus? gameStatus, List<int> seriesIds, List<int> gameIds, int page)
        {
            var hasTournaments = tournamentIds != null && tournamentIds.Any();
            var hasSeries = seriesIds != null && seriesIds.Any();

            if (hasTournaments == false && hasSeries == false)
            {
                throw new ArgumentException($"Provide either {nameof(tournamentIds)} or {nameof(seriesIds)}.", nameof(tournamentIds));
            }

            var request = BaseRequest
                .AppendPathSegment("games")
                .SetQueryParam("page", page);

            if (hasTournaments)
            {
                request = request.SetQueryParam("tournaments", JoinCappedIds(tournamentIds, 25, nameof(tournamentIds)));
            }

            if (hasSeries)
            {
                request = request.SetQueryParam("series", JoinCappedIds(seriesIds, 5, nameof(seriesIds)));
            }

            if (gameIds != null && gameIds.Any())
            {
                request = request.SetQueryParam("ids", JoinCappedIds(gameIds, 50, nameof(gameIds)));
            }

            if (gameStatus.HasValue)
            {
                request = request.SetQueryParam("status", gameStatus.Value.ToString().ToLower());
            }

            if (bank.HasValue)
            {
                request = request.SetQueryParam("bank", bank.Value);
            }

            if (round.HasValue)
            {
                request = request.SetQueryParam("round", round.Value);
            }

            if (arenaId.HasValue)
            {
                request = request.SetQueryParam("arena", arenaId.Value);
            }

            if (playerId.HasValue)
            {
                request = request.SetQueryParam("player", playerId.Value);
            }

            return request;
        }

        /// <summary>
        /// Get the multi-player games of one or more tournaments or series.
        /// </summary>
        /// <param name="tournamentIds">Up to 25 tournament ids. Provide this or <paramref name="seriesIds"/>.</param>
        /// <param name="playerId">Only return games this player played.</param>
        /// <param name="arenaId">Only return games on this arena.</param>
        /// <param name="round">Only return games in this round.</param>
        /// <param name="bank">Only return games on this bank.</param>
        /// <param name="gameStatus">Only return games in this state.</param>
        /// <param name="seriesIds">Up to 5 series ids. Provide this or <paramref name="tournamentIds"/>.</param>
        /// <param name="gameIds">Up to 50 game ids to fetch.</param>
        /// <param name="page">Which page of results to get.</param>
        /// <param name="cancellationToken">Cancels the call.</param>
        public async Task<List<Game>> GetGames(List<int> tournamentIds = null, int? playerId = null, int? arenaId = null, int? round = null,
                                               int? bank = null, GameStatus? gameStatus = null, List<int> seriesIds = null,
                                               List<int> gameIds = null, int page = 1, CancellationToken cancellationToken = default)
        {
            return await GetData<List<Game>>(GamesRequest(tournamentIds, playerId, arenaId, round, bank, gameStatus, seriesIds, gameIds, page),
                                             cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetGames"/>.
        /// </summary>
        public IAsyncEnumerable<Game> EnumerateGames(List<int> tournamentIds = null, int? playerId = null, int? arenaId = null, int? round = null,
                                                     int? bank = null, GameStatus? gameStatus = null, List<int> seriesIds = null,
                                                     List<int> gameIds = null, CancellationToken cancellationToken = default)
        {
            return Enumerate<Game>(page => GamesRequest(tournamentIds, playerId, arenaId, round, bank, gameStatus, seriesIds, gameIds, page),
                                   cancellationToken);
        }

        /// <summary>
        /// Get one game of a tournament, with the player objects attached.
        /// </summary>
        public async Task<TournamentGame> GetGame(int tournamentId, int gameId, CancellationToken cancellationToken = default)
        {
            return await GetData<TournamentGame>(BaseRequest
                .AppendPathSegment("tournaments")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("games")
                .AppendPathSegment(gameId), cancellationToken);
        }

        /// <summary>
        /// Get every game of one tournament, with the player objects attached.
        /// </summary>
        public async Task<List<TournamentGame>> GetTournamentGames(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetData<List<TournamentGame>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("games"), cancellationToken);
        }

        private IFlurlRequest SinglePlayerGamesRequest(int tournamentId, int page, int? limit, List<int> gameIds,
                                                       SinglePlayerGameStatus? status, bool? bestGame, bool? voided,
                                                       int? round, int? playerId, int? arenaId)
        {
            var request = BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("single-player-games")
                            .SetQueryParam("page", page);

            if (limit.HasValue)
            {
                request = request.SetQueryParam("limit", limit.Value);
            }

            if (gameIds != null && gameIds.Any())
            {
                request = request.SetQueryParam("ids", JoinCappedIds(gameIds, 50, nameof(gameIds)));
            }

            if (status.HasValue)
            {
                request = request.SetQueryParam("status", status.Value.ToString().ToLower());
            }

            if (bestGame.HasValue)
            {
                request = request.SetQueryParam("bestGame", bestGame.Value ? 1 : 0);
            }

            if (voided.HasValue)
            {
                request = request.SetQueryParam("voided", voided.Value ? 1 : 0);
            }

            if (round.HasValue)
            {
                request = request.SetQueryParam("round", round.Value);
            }

            if (playerId.HasValue)
            {
                request = request.SetQueryParam("player", playerId.Value);
            }

            if (arenaId.HasValue)
            {
                request = request.SetQueryParam("arena", arenaId.Value);
            }

            return request;
        }

        /// <summary>
        /// Get the single player games of a tournament. Best game and pingolf formats use these.
        /// </summary>
        /// <param name="tournamentId">The tournament to read.</param>
        /// <param name="page">Which page of results to get.</param>
        /// <param name="limit">How many games to get. The API defaults to 25 and allows up to 500.</param>
        /// <param name="gameIds">Up to 50 game ids to fetch.</param>
        /// <param name="status">Only return games in this state.</param>
        /// <param name="bestGame">Only return games marked as a best game.</param>
        /// <param name="voided">Include voided games.</param>
        /// <param name="round">Only return games in this round.</param>
        /// <param name="playerId">Only return games this player played.</param>
        /// <param name="arenaId">Only return games on this arena.</param>
        /// <param name="cancellationToken">Cancels the call.</param>
        public async Task<List<SinglePlayerGame>> GetSinglePlayerGames(int tournamentId, int page = 1, int? limit = null, List<int> gameIds = null,
                                                                       SinglePlayerGameStatus? status = null, bool? bestGame = null, bool? voided = null,
                                                                       int? round = null, int? playerId = null, int? arenaId = null,
                                                                       CancellationToken cancellationToken = default)
        {
            return await GetData<List<SinglePlayerGame>>(
                SinglePlayerGamesRequest(tournamentId, page, limit, gameIds, status, bestGame, voided, round, playerId, arenaId),
                cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetSinglePlayerGames"/>.
        /// </summary>
        public IAsyncEnumerable<SinglePlayerGame> EnumerateSinglePlayerGames(int tournamentId, int? limit = null, List<int> gameIds = null,
                                                                             SinglePlayerGameStatus? status = null, bool? bestGame = null,
                                                                             bool? voided = null, int? round = null, int? playerId = null,
                                                                             int? arenaId = null, CancellationToken cancellationToken = default)
        {
            return Enumerate<SinglePlayerGame>(
                page => SinglePlayerGamesRequest(tournamentId, page, limit, gameIds, status, bestGame, voided, round, playerId, arenaId),
                cancellationToken);
        }

        public async Task<SinglePlayerGame> GetSinglePlayerGame(int tournamentId, int singlePlayerGameId, CancellationToken cancellationToken = default)
        {
            return await GetData<SinglePlayerGame>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("single-player-games")
                            .AppendPathSegment(singlePlayerGameId), cancellationToken);
        }

        /// <summary>
        /// Get the leading scores on each arena of a tournament.
        /// </summary>
        public async Task<List<SinglePlayerGame>> GetTopScoresByArena(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetData<List<SinglePlayerGame>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("single-player-games/top-scores"), cancellationToken);
        }

        private IFlurlRequest CardsRequest(int tournamentId, int page, int? limit, SinglePlayerGameStatus? status,
                                           bool? bestGame, bool? voided, int? playerId)
        {
            var request = BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("cards")
                            .SetQueryParam("page", page);

            if (limit.HasValue)
            {
                request = request.SetQueryParam("limit", limit.Value);
            }

            if (status.HasValue)
            {
                request = request.SetQueryParam("status", status.Value.ToString().ToLower());
            }

            if (bestGame.HasValue)
            {
                request = request.SetQueryParam("bestGame", bestGame.Value ? 1 : 0);
            }

            if (voided.HasValue)
            {
                request = request.SetQueryParam("voided", voided.Value ? 1 : 0);
            }

            if (playerId.HasValue)
            {
                request = request.SetQueryParam("player", playerId.Value);
            }

            return request;
        }

        /// <summary>
        /// Get the cards of a tournament. Card-based best game tournaments use these.
        /// </summary>
        /// <param name="tournamentId">The tournament to read.</param>
        /// <param name="page">Which page of results to get.</param>
        /// <param name="limit">How many cards to get. The API defaults to 25 and allows up to 500.</param>
        /// <param name="status">Only return cards in this state.</param>
        /// <param name="bestGame">Only return cards marked as a best game.</param>
        /// <param name="voided">Include voided cards.</param>
        /// <param name="playerId">Only return cards for this player.</param>
        /// <param name="cancellationToken">Cancels the call.</param>
        public async Task<List<Card>> GetCards(int tournamentId, int page = 1, int? limit = null, SinglePlayerGameStatus? status = null,
                                               bool? bestGame = null, bool? voided = null, int? playerId = null,
                                               CancellationToken cancellationToken = default)
        {
            return await GetData<List<Card>>(CardsRequest(tournamentId, page, limit, status, bestGame, voided, playerId), cancellationToken);
        }

        /// <summary>
        /// Walk every page of <see cref="GetCards"/>.
        /// </summary>
        public IAsyncEnumerable<Card> EnumerateCards(int tournamentId, int? limit = null, SinglePlayerGameStatus? status = null,
                                                     bool? bestGame = null, bool? voided = null, int? playerId = null,
                                                     CancellationToken cancellationToken = default)
        {
            return Enumerate<Card>(page => CardsRequest(tournamentId, page, limit, status, bestGame, voided, playerId), cancellationToken);
        }

        public async Task<Card> GetCard(int tournamentId, int cardId, CancellationToken cancellationToken = default)
        {
            return await GetData<Card>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("cards")
                            .AppendPathSegment(cardId), cancellationToken);
        }

        #endregion

        #region formats & statistics

        public async Task<FlipFrenzy> GetFlipFrenzy(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<FlipFrenzy>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("frenzy"), cancellationToken);
        }

        public async Task<MaxMatchplay> GetMaxMatchplay(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<MaxMatchplay>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("max-matchplay"), cancellationToken);
        }

        /// <summary>
        /// Get the statistics of a match play format tournament.
        /// </summary>
        public async Task<MatchplayStats> GetMatchplayStats(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<MatchplayStats>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/matchplay"), cancellationToken);
        }

        /// <summary>
        /// Get the per round statistics of a tournament.
        /// </summary>
        public async Task<List<RoundStats>> GetRoundStats(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<List<RoundStats>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/rounds"), cancellationToken);
        }

        /// <summary>
        /// Get the per arena statistics of a tournament, such as how long games take.
        /// </summary>
        public async Task<List<ArenaStats>> GetArenaStats(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<List<ArenaStats>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/arenas"), cancellationToken);
        }

        /// <summary>
        /// Get the per player statistics of a tournament.
        /// </summary>
        public async Task<PlayerStats> GetPlayerStats(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<PlayerStats>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/players"), cancellationToken);
        }

        /// <summary>
        /// Get the match duration statistics of a tournament.
        /// </summary>
        /// <remarks>
        /// The endpoint answers only for a tournament that has a set duration. For any other
        /// tournament MatchPlay returns HTTP 400 with the message "This tournament does not have a
        /// definite duration." and this method raises <see cref="PinballApiException"/>.
        /// </remarks>
        public async Task<List<ArenaStats>> GetMatchStats(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<List<ArenaStats>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/matches"), cancellationToken);
        }

        public async Task<BestGameStats> GetBestGameStats(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<BestGameStats>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/bestgame"), cancellationToken);
        }

        public async Task<List<BestGameSummary>> GetBestGameSummary(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await GetJson<List<BestGameSummary>>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("arenas/bgsummary"), cancellationToken);
        }

        public async Task<BestGame> GetBestGameDetails(int tournamentId, int arenaId, CancellationToken cancellationToken = default)
        {
            return await GetJson<BestGame>(BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("arenas")
                            .AppendPathSegment(arenaId)
                            .AppendPathSegment("bgdetails"), cancellationToken);
        }

        #endregion
    }
}
