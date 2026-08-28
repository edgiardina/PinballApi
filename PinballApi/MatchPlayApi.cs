using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using PinballApi.Converters;
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
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi
{
    /// <summary>
    /// The read-only MatchPlay Events API. It also serves the OPDB machine database and PinTips.
    /// </summary>
    /// <remarks>
    /// Get an API token at https://app.matchplay.events/account/tokens. MatchPlay rate limits most
    /// endpoints to 120 requests per minute and some, such as search, far below that. Cache what
    /// you fetch, and prefer the bulk exports over a loop of single-entry calls.
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

        public async Task<List<Arena>> GetArenas(Status status = Status.Active, List<string> arenaIds = null, int page = 1)
        {
            var request = BaseRequest
                .AppendPathSegment("arenas")
                .SetQueryParam("status", status.ToString().ToLower())
                .SetQueryParam("page", page);

            if (arenaIds != null && arenaIds.Any())
            {
                request = request.SetQueryParam("arenas", string.Join(",", arenaIds));
            }

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Arena>>(JsonSerializerOptions);
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
        public async Task<List<Game>> GetGames(List<int> tournamentIds = null, int? playerId = null, int? arenaId = null, int? round = null, int? bank = null, GameStatus? gameStatus = null, List<int> seriesIds = null, List<int> gameIds = null, int page = 1)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Game>>(JsonSerializerOptions);
        }

        public async Task<Game> GetGame(int tournamentId, int gameId)
        {
            var json = await BaseRequest
                .AppendPathSegment("tournaments")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("games")
                .AppendPathSegment(gameId)
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<Game>(JsonSerializerOptions);
        }

        /// <summary>
        /// Estimate the WPPR value of a tournament or a series.
        /// </summary>
        public async Task<IfpaEstimate> GetIfpaEstimate(int? tournamentId = null, int? seriesId = null, List<int> ifpaIds = null, List<string> names = null)
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

            var response = await request.PostAsync();
            return await response.GetJsonAsync<IfpaEstimate>();
        }

        public async Task<List<Location>> GetLocations(Status? status = null, List<int> locationIds = null, int page = 1)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Location>>(JsonSerializerOptions);
        }

        public async Task<List<Player>> GetPlayers(Status? status = null, List<int> players = null, int page = 1)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Player>>(JsonSerializerOptions);
        }

        public async Task<User> GetMyProfile()
        {
            var json = await BaseRequest
                            .AppendPathSegment("users")
                            .AppendPathSegment("profile")
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<User>(JsonSerializerOptions);
        }

        public async Task<UserProfile> GetProfile(int playerId)
        {
            return await BaseRequest
                            .AppendPathSegment("users")
                            .AppendPathSegment(playerId)
                            .SetQueryParam("includeIfpa", "true")
                            .SetQueryParam("includeCounts", "true")
                            .GetJsonAsync<UserProfile>();
        }


        public async Task<List<User>> SearchForUsers(string searchText)
        {
            var json = await BaseRequest
                .AppendPathSegment("search")
                .SetQueryParam("query", searchText)
                .SetQueryParam("type", "users")
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<User>>(JsonSerializerOptions);
        }

        public async Task<List<Tournament>> SearchForTournaments(string searchText)
        {
            var json = await BaseRequest
                .AppendPathSegment("search")
                .SetQueryParam("query", searchText)
                .SetQueryParam("type", "tournaments")
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Tournament>>(JsonSerializerOptions);
        }

        #region series
        public async Task<List<Series>> GetSeries(int? ownerUserId = null, int? playedUserId = null, SeriesStatus? seriesStatus = null, int page = 1)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Series>>(JsonSerializerOptions);
        }

        public async Task<Series> GetSeries(int seriesId)
        {
            var json = await BaseRequest
                           .AppendPathSegment("series")
                           .AppendPathSegment(seriesId)
                           .SetQueryParam("includeDetails", true)
                           .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<Series>(JsonSerializerOptions);
        }

        public async Task<List<Player>> GetSeriesAttendance(int seriesId, int count)
        {
            var json = await BaseRequest
                           .AppendPathSegment("series")
                           .AppendPathSegment(seriesId)
                           .AppendPathSegment("stats")
                           .AppendPathSegment("attendance")
                           .SetQueryParam("count", count)
                           .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Player>>(JsonSerializerOptions);
        }

        public async Task<SeriesStats> GetSeriesStats(int seriesId)
        {
            return await BaseRequest
                            .AppendPathSegment("series")
                            .AppendPathSegment(seriesId)
                            .AppendPathSegment("stats")
                            .GetJsonAsync<SeriesStats>();
        }

        #endregion

        #region ratings
        public async Task<RatingComparison> ComparePlayers(List<int> playerIds = null, List<int> ifpaIds = null, List<int> userIds = null)
        {
            var request = BaseRequest
                .AppendPathSegment("ratings/compare");

            if (playerIds != null && playerIds.Any())
            {
                if (playerIds.Count > 24)
                    throw new ArgumentException($"{nameof(playerIds)} cannot have more than 24 items", nameof(playerIds));
            }

            if (ifpaIds != null && ifpaIds.Any())
            {
                if (ifpaIds.Count > 24)
                    throw new ArgumentException($"{nameof(ifpaIds)} cannot have more than 24 items", nameof(ifpaIds));
            }

            if (userIds != null && userIds.Any())
            {
                if (userIds.Count > 24)
                    throw new ArgumentException($"{nameof(userIds)} cannot have more than 24 items", nameof(userIds));
            }

            return await request.PostJsonAsync(
                                new
                                {
                                    ifpaIds = ifpaIds,
                                    playerIds = playerIds,
                                    userIds = userIds
                                }).ReceiveJson<RatingComparison>();
        }

        public async Task<RatingProfile> GetRatingProfile(int id, RatingQueryType ratingQueryType)
        {
            return await BaseRequest
                .AppendPathSegment("ratings")
                .AppendPathSegment(ratingQueryType.ToString().ToLower())
                .AppendPathSegment(id)
                .GetJsonAsync<RatingProfile>();
        }


        public async Task<List<Rating>> GetCurrentRatingData(List<int> ifpaIds = null, List<int> userIds = null, int page = 1)
        {
            GetCurrentRatingDataPayload data = new GetCurrentRatingDataPayload(ifpaIds, userIds);

            if (ifpaIds != null && ifpaIds.Any())
            {
                if (ifpaIds.Count > 24)
                    throw new ArgumentException($"{nameof(ifpaIds)} cannot have more than 24 items", nameof(ifpaIds));
            }

            if (userIds != null && userIds.Any())
            {
                if (userIds.Count > 24)
                    throw new ArgumentException($"{nameof(userIds)} cannot have more than 24 items", nameof(userIds));
            }

            var json = await BaseRequest
                .AppendPathSegment("ratings")
                .SetQueryParam("page", page)
                .SendJsonAsync(HttpMethod.Get, data)
                .ReceiveString();

            return JsonNode.Parse(json)["data"].Deserialize<List<Rating>>(JsonSerializerOptions);
        }

        public async Task<List<RatingPeriod>> GetRatingPeriods(int page = 1)
        {
            var json = await BaseRequest
               .AppendPathSegment("rating-periods")
               .SetQueryParam("page", page)
               .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<RatingPeriod>>(JsonSerializerOptions);
        }

        public async Task<List<IfpaRatingHistory>> GetRatingHistoryByIfpaId(int ifpaId, int limit = 100, int page = 1)
        {
            var json = await BaseRequest
               .AppendPathSegment("ifpa")
               .AppendPathSegment(ifpaId)
               .AppendPathSegment("rating-history")
               .SetQueryParam("page", page)
               .SetQueryParam("limit", limit)
               .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<IfpaRatingHistory>>(JsonSerializerOptions);
        }

        public async Task<SingleRatingPeriod> GetRatingPeriod(DateTime date)
        {
            return await BaseRequest
                           .AppendPathSegment("rating-periods")
                           .AppendPathSegment(date.ToString("yyyy-MM-dd"))
                           .GetJsonAsync<SingleRatingPeriod>();
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
        public async Task<List<Player>> ResolveUnknownPlayers(List<int> playerIds)
        {
            var json = await BaseRequest
                .AppendPathSegment("players")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("players", JoinResolveIds(playerIds, nameof(playerIds)))
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Player>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get the arena objects for a set of arena ids.
        /// </summary>
        /// <param name="arenaIds">Up to 25 arena ids.</param>
        public async Task<List<Arena>> ResolveUnknownArenas(List<int> arenaIds)
        {
            var json = await BaseRequest
                .AppendPathSegment("arenas")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("arenas", JoinResolveIds(arenaIds, nameof(arenaIds)))
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Arena>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get the profile objects for a set of user ids.
        /// </summary>
        /// <param name="userIds">Up to 25 user ids.</param>
        public async Task<List<User>> ResolveUnknownUsers(List<int> userIds)
        {
            var json = await BaseRequest
                .AppendPathSegment("users")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("users", JoinResolveIds(userIds, nameof(userIds)))
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<User>>(JsonSerializerOptions);
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
        public async Task<List<Player>> ResolveUnknownTournamentPlayers(int tournamentId, List<int> playerIds)
        {
            var json = await BaseRequest
                .AppendPathSegment("tournaments")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("players")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("players", JoinResolveIds(playerIds, nameof(playerIds)))
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Player>>(JsonSerializerOptions);
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
        public async Task<List<Arena>> ResolveUnknownTournamentArenas(int tournamentId, List<int> arenaIds)
        {
            var json = await BaseRequest
                .AppendPathSegment("tournaments")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("arenas")
                .AppendPathSegment("resolve-unknown")
                .SetQueryParam("arenas", JoinResolveIds(arenaIds, nameof(arenaIds)))
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Arena>>(JsonSerializerOptions);
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
        /// <remarks>
        /// Do not call this in a loop to build a local catalog. Download <see cref="GetOpdbExport"/>
        /// once instead, and store the result.
        /// </remarks>
        public async Task<OpdbEntry> GetOpdbEntry(string opdbId, bool includePeople = false, bool includeImages = false)
        {
            var request = BaseRequest
                .AppendPathSegment("opdb")
                .AppendPathSegment("entry")
                .AppendPathSegment(opdbId);

            request = SetIncludeFlag(request, "includePeople", includePeople);
            request = SetIncludeFlag(request, "includeImages", includeImages);

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<OpdbEntry>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get every OPDB id that was moved or removed. Use it to repair ids you stored earlier.
        /// </summary>
        public async Task<List<OpdbChangelogEntry>> GetOpdbChangelog()
        {
            var json = await BaseRequest
                .AppendPathSegment("opdb")
                .AppendPathSegment("changelog")
                .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<OpdbChangelogEntry>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get the PinTips for one OPDB entry.
        /// </summary>
        /// <param name="opdbId">The OPDB id to get tips for.</param>
        public async Task<PinTipsResult> GetPinTipsByOpdbId(string opdbId)
        {
            return await BaseRequest
                .AppendPathSegment("pintips")
                .SetQueryParam("opdbId", opdbId)
                .GetJsonAsync<PinTipsResult>();
        }

        /// <summary>
        /// Get the PinTips for the machine behind a Match Play arena.
        /// </summary>
        /// <param name="arenaId">The Match Play arena to get tips for.</param>
        public async Task<PinTipsResult> GetPinTipsByArenaId(int arenaId)
        {
            return await BaseRequest
                .AppendPathSegment("pintips")
                .SetQueryParam("arenaId", arenaId)
                .GetJsonAsync<PinTipsResult>();
        }

        /// <summary>
        /// Download the full OPDB data set from the Match Play CDN.
        /// </summary>
        /// <remarks>
        /// The download is several megabytes and needs no API token. Fetch it on a schedule,
        /// store the result, and serve searches and typeaheads from your own store.
        /// </remarks>
        public async Task<List<OpdbEntry>> GetOpdbExport()
        {
            var export = await GetExport<OpdbExport>(OpdbExportUrl);

            return export?.Entries;
        }

        /// <summary>
        /// Download the cut down OPDB data set from the Match Play CDN.
        /// </summary>
        /// <remarks>
        /// Use this when you only need names and backglass images, for example to build a
        /// machine picker. It needs no API token.
        /// </remarks>
        public async Task<List<OpdbSlimEntry>> GetOpdbSlimExport()
        {
            var export = await GetExport<OpdbSlimExport>(OpdbSlimExportUrl);

            return export?.Entries;
        }

        /// <summary>
        /// Download the full PinTips data set from the Match Play CDN. It needs no API token.
        /// </summary>
        public async Task<List<PinTip>> GetPinTipsExport()
        {
            return await GetExport<List<PinTip>>(PinTipsExportUrl);
        }

        private async Task<T> GetExport<T>(string url)
        {
            using (var stream = await url.GetStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptions);
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
        /// <summary>
        /// Get a list of tournaments.
        /// </summary>
        /// <remarks>
        /// Do not page through thousands of tournaments with this. Contact MatchPlay if you need
        /// bulk data.
        /// </remarks>
        /// <param name="limit">Results per page. The API defaults to 25 and allows up to 100.</param>
        public async Task<List<Tournament>> GetTournaments(int? ownerUserId = null, int? playedUserId = null, TournamentStatus? status = null, int? seriesId = null, int page = 1, int? limit = null)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Tournament>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get one tournament, and optionally the related objects.
        /// </summary>
        /// <remarks>
        /// The endpoint reads each include flag by presence, not by value. Sending
        /// <c>includePlayers=false</c> still returns the players. This method therefore sends a
        /// flag only when it is true. Ask for what you need. Each extra flag costs response size.
        /// </remarks>
        public async Task<Tournament> GetTournament(int tournamentId, bool includePlayers = false, bool includeArenas = false, bool includeBanks = false, bool includeScorekeepers = false, bool includeSeries = false,
                                                    bool includeLocation = false, bool includeRsvpConfiguration = false, bool includeParent = false, bool includePlayoffs = false, bool includeShortcut = false,
                                                    bool includeEntryConfiguration = false, bool includeLinkedTournaments = false, bool includeEvent = false)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<Tournament>(JsonSerializerOptions);
        }

        /// <summary>
        /// Add an include flag only when it is true. MatchPlay reads these flags by presence, so
        /// sending the value "false" still turns the include on.
        /// </summary>
        private static IFlurlRequest SetIncludeFlag(IFlurlRequest request, string name, bool value)
        {
            return value ? request.SetQueryParam(name, 1) : request;
        }

        public async Task<FlipFrenzy> GetFlipFrenzy(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("frenzy")
                            .GetJsonAsync<FlipFrenzy>();
        }

        public async Task<MaxMatchplay> GetMaxMatchplay(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("max-matchplay")
                            .GetJsonAsync<MaxMatchplay>();
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
        public async Task<List<SinglePlayerGame>> GetSinglePlayerGames(int tournamentId, int page = 1, int? limit = null, List<int> gameIds = null,
                                                                      SinglePlayerGameStatus? status = null, bool? bestGame = null, bool? voided = null,
                                                                      int? round = null, int? playerId = null, int? arenaId = null)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<SinglePlayerGame>>(JsonSerializerOptions);
        }

        public async Task<SinglePlayerGame> GetSinglePlayerGame(int tournamentId, int singlePlayerGameId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("single-player-games")
                            .AppendPathSegment(singlePlayerGameId)
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<SinglePlayerGame>(JsonSerializerOptions);
        }

        public async Task<List<SinglePlayerGame>> GetTopFiveScoresByArena(int tournamentId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("single-player-games/top-scores")
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<SinglePlayerGame>>(JsonSerializerOptions);
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
        public async Task<List<Card>> GetCards(int tournamentId, int page = 1, int? limit = null, SinglePlayerGameStatus? status = null,
                                               bool? bestGame = null, bool? voided = null, int? playerId = null)
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

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Card>>(JsonSerializerOptions);
        }

        public async Task<Card> GetCard(int tournamentId, int cardId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("cards")
                            .AppendPathSegment(cardId)
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<Card>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get the play queues of a tournament. Flip Frenzy uses these.
        /// </summary>
        /// <remarks>Returns an empty list for a format that does not queue players.</remarks>
        public async Task<List<Queue>> GetQueues(int tournamentId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("queues")
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Queue>>(JsonSerializerOptions);
        }

        public async Task<MatchplayStats> GetMatchplayStats(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/matchplay")
                            .GetJsonAsync<MatchplayStats>();
        }

        public async Task<List<RoundStats>> GetMatchplayRoundStats(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/rounds")
                            .GetJsonAsync<List<RoundStats>>();
        }

        public async Task<List<ArenaStats>> GetMatchplayArenaStats(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/arenas")
                            .GetJsonAsync<List<ArenaStats>>();
        }

        public async Task<PlayerStats> GetMatchplayPlayerStats(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/players")
                            .GetJsonAsync<PlayerStats>();
        }

        /// <summary>
        /// Get the match duration statistics of a tournament.
        /// </summary>
        /// <remarks>
        /// The endpoint answers only for a tournament that has a set duration. For any other
        /// tournament MatchPlay returns HTTP 400 with the message "This tournament does not have a
        /// definite duration." and Flurl raises a <see cref="Flurl.Http.FlurlHttpException"/>.
        /// </remarks>
        public async Task<List<ArenaStats>> GetMatchplayMatchesStats(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/matches")
                            .GetJsonAsync<List<ArenaStats>>();
        }

        public async Task<List<MatchplayGames>> GetMatchplayGames(int tournamentId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("games")
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<MatchplayGames>>(JsonSerializerOptions);
        }

        public async Task<MatchplayGames> GetMatchplayGame(int tournamentId, int gameId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("games")
                            .AppendPathSegment(gameId)
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<MatchplayGames>(JsonSerializerOptions);
        }

        public async Task<BestGameStats> GetBestGameStats(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("stats/bestgame")
                            .GetJsonAsync<BestGameStats>();
        }


        public async Task<List<BestGameSummary>> GetBestGameSummary(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("arenas/bgsummary")
                            .GetJsonAsync<List<BestGameSummary>>();
        }

        public async Task<BestGame> GetBestGameDetails(int tournamentId, int arenaId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("arenas")
                            .AppendPathSegment(arenaId)
                            .AppendPathSegment("bgdetails")
                            .GetJsonAsync<BestGame>();
        }

        public async Task<List<Round>> GetRounds(int tournamentId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("rounds")
                            .GetStringAsync();


            return JsonNode.Parse(json)["data"].Deserialize<List<Round>>(JsonSerializerOptions);
        }

        public async Task<List<Standing>> GetStandings(int tournamentId)
        {
            return await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("standings")
                            .GetJsonAsync<List<Standing>>();
        }

        /// <summary>
        /// Get one entry per arena in a tournament, with the number of games played on it.
        /// </summary>
        /// <remarks>
        /// MatchPlay builds this only after the tournament is complete. It returns an empty list
        /// for a tournament that is still open.
        /// </remarks>
        public async Task<List<TournamentArenaSummary>> GetTournamentArenaSummary(int tournamentId, int page = 1)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("summary")
                            .AppendPathSegment("arenas")
                            .SetQueryParam("page", page)
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<TournamentArenaSummary>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get one entry per player and arena pair in a tournament, with wins and losses.
        /// </summary>
        /// <remarks>
        /// MatchPlay builds this only after the tournament is complete. It returns an empty list
        /// for a tournament that is still open.
        /// </remarks>
        public async Task<List<TournamentPlayerArenaSummary>> GetTournamentPlayerArenaSummary(int tournamentId, int page = 1)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("summary")
                            .AppendPathSegment("player-arenas")
                            .SetQueryParam("page", page)
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<TournamentPlayerArenaSummary>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Get one entry per player, opponent and arena combination in a tournament.
        /// </summary>
        /// <remarks>
        /// MatchPlay builds this only after the tournament is complete. The data is duplicated on
        /// purpose. Each pair of players appears once from each side.
        /// </remarks>
        public async Task<List<TournamentMatchSummary>> GetTournamentMatchSummary(int tournamentId, int page = 1)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("summary")
                            .AppendPathSegment("matches")
                            .SetQueryParam("page", page)
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<TournamentMatchSummary>>(JsonSerializerOptions);
        }

        #endregion
    }
}
