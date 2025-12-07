using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using PinballApi.Converters;
using PinballApi.Models.MatchPlay;
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
    public class MatchPlayApi
    {
        protected readonly string ApiToken;

        protected readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        protected IFlurlRequest BaseRequest => $"https://app.matchplay.events/api/"
                                                    .WithOAuthBearerToken(ApiToken)
                                                    .WithHeader("Content-Type", "application/json")
                                                    .WithHeader("Accept", "application/json")
                                                    .WithSettings(settings =>
                                                    {
                                                        settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions);
                                                    });

        public MatchPlayApi(string apiToken)
        {
            this.ApiToken = apiToken;
        }

        [Obsolete("Dashboard method has been removed")]
        public async Task<Dashboard> GetDashboard()
        {
            return await BaseRequest
                .AppendPathSegment("dashboard")
                .GetJsonAsync<Dashboard>();
        }

        public async Task<List<Arena>> GetArenas(Status status = Status.Active, List<string> arenaIds = null, int page = 1)
        {
            var request = BaseRequest
                .AppendPathSegment("arenas")
                .SetQueryParam("status", status.ToString().ToLower())
                .SetQueryParam("page", page);

            if (arenaIds != null && arenaIds.Any())
            {
                request = request.SetQueryParams("arenas", string.Join(",", arenaIds));
            }

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<List<Arena>>(JsonSerializerOptions);
        }

        public async Task<List<Game>> GetGames(List<int> tournamentIds, int? playerId = null, int? arenaId = null, int? round = null, int? bank = null, GameStatus? gameStatus = null)
        {
            var request = BaseRequest
                .AppendPathSegment("games")
                .SetQueryParam("tournaments", string.Join(",", tournamentIds));

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

        public async Task<IfpaEstimate> GetIfpaEstimate(int? touramentId = null, int? seriesId = null, List<int> ifpaIds = null, List<string> names = null)
        {
            if (touramentId.HasValue == false && seriesId.HasValue == false && ifpaIds == null && names == null)
            {
                throw new ArgumentException("Provide EITHER a tournament id OR a series id OR a list of ifpaIds/names.");
            }

            var request = BaseRequest
                .AppendPathSegment("ifpa/wppr-estimator");

            if (touramentId.HasValue)
            {
                request = request.SetQueryParam("tournamentId", touramentId.Value);
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
                .SetQueryParam("page", page); ;

            if (locationIds != null && locationIds.Any())
            {
                request = request.SetQueryParams("locations", string.Join(",", locationIds));
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
                request = request.SetQueryParams("players", string.Join(",", players));
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

        #region tournaments
        public async Task<List<Tournament>> GetTournaments(int? ownerUserId = null, int? playedUserId = null, TournamentStatus? status = null, int? seriesId = null, int page = 1)
        {
            var request = BaseRequest
                .AppendPathSegment("tournaments")
                .SetQueryParam("type", "tournaments")
                .SetQueryParam("page", page);

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

        public async Task<Tournament> GetTournament(int tournamentId, bool includePlayers = false, bool includeArenas = false, bool includeBanks = false, bool includeScorekeepers = false, bool includeSeries = false,
                                                    bool includeLocation = false, bool includeRsvpConfiguration = false, bool includeParent = false, bool includePlayoffs = false, bool includeShortcut = false)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .SetQueryParam("includePlayers", includePlayers)
                            .SetQueryParam("includeArenas", includeArenas)
                            .SetQueryParam("includeBanks", includeBanks)
                            .SetQueryParam("includeScorekeepers", includeScorekeepers)
                            .SetQueryParam("includeSeries", includeSeries)
                            .SetQueryParam("includeLocation", includeLocation)
                            .SetQueryParam("includeRsvpConfiguration", includeRsvpConfiguration)
                            .SetQueryParam("includeParent", includeParent)
                            .SetQueryParam("includePlayoffs", includePlayoffs)
                            .SetQueryParam("includeShortcut", includeShortcut)
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<Tournament>(JsonSerializerOptions);
        }

        public async Task<Tournament> GetAmazingRace(int tournamentId)
        {
            throw new NotImplementedException();
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

        public async Task<List<SinglePlayerGame>> GetSinglePlayerGames(int tournamentId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("single-player-games")
                            .GetStringAsync();

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

        public async Task<List<Card>> GetCards(int tournamentId)
        {
            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("cards")
                            .GetStringAsync();

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

        //TODO: find out shape of Queue return
        public async Task<Card> GetQueues(int tournamentId)
        {
            throw new NotImplementedException();

            var json = await BaseRequest
                            .AppendPathSegment("tournaments")
                            .AppendPathSegment(tournamentId)
                            .AppendPathSegment("queues")
                            .GetStringAsync();

            return JsonNode.Parse(json)["data"].Deserialize<Card>();
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

        public async Task<List<ArenaStats>> GetMatchplayMatchesStats(int tournamentId)
        {
            throw new NotImplementedException("This tournament does not have a definite duration. Give the tournament a specific duration.");

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

        #endregion
    }
}
