using Flurl;
using Flurl.Http;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using PinballApi.Models.MatchPlay;
using PinballApi.Models.MatchPlay.SeriesStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi
{
    public class MatchPlayApi
    {
        protected readonly string ApiToken;
        protected IFlurlRequest BaseRequest => $"https://next.matchplay.events/api/"
                                                              .WithOAuthBearerToken(ApiToken)
                                                              .WithHeader("Content-Type", "application/json")
                                                              .WithHeader("Accept", "application/json");

        public MatchPlayApi(string apiToken)
        {
            this.ApiToken = apiToken;
        }

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

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Arena>>();
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

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Game>>();
        }

        public async Task<IfpaEstimate> GetIfpaEstimate(int? touramentId = null, int? seriesId = null, List<int> ifpaIds = null, List<string> names = null)
        {
            if(touramentId.HasValue == false && seriesId.HasValue == false && ifpaIds == null && names == null)
            {
                throw new ArgumentException("Provide EITHER a tournament id OR a series id OR a list of ifpaIds/names.");
            }

            var request = BaseRequest
                .AppendPathSegment("ifpa/wppr-estimator");

            if(touramentId.HasValue)
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

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Location>>();
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

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Player>>();
        }

        public async Task<User> GetMyProfile()
        {
            var json = await BaseRequest
                            .AppendPathSegment("users")
                            .AppendPathSegment("profile")
                            .GetStringAsync();

            return JObject.Parse(json)
               .SelectToken("data", false).ToObject<User>();
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

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<User>>();
        }

        public async Task<List<Tournament>> SearchForTournaments(string searchText)
        {
            var json = await BaseRequest
                .AppendPathSegment("search")
                .SetQueryParam("query", searchText)
                .SetQueryParam("type", "tournaments")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Tournament>>();
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

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Series>>();
        }

        public async Task<Series> GetSeries(int seriesId)
        {
            var json = await BaseRequest
                           .AppendPathSegment("series")
                           .AppendPathSegment(seriesId)
                           .SetQueryParam("includeDetails", true)
                           .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<Series>();
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

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Player>>();
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

        #region Ratings
        public async Task<RatingComparison> ComparePlayers(List<int> playerIds = null, List<int> ifpaIds = null, List<int> userIds = null)
        {
            var request = BaseRequest
                .AppendPathSegment("ratings/compare");

            if(playerIds != null && playerIds.Any())
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

            var response = await request.PostJsonAsync(
                                new { 
                                    ifpaIds = ifpaIds, 
                                    playerIds = playerIds, 
                                    userIds = userIds 
                                });
            return await response.GetJsonAsync<RatingComparison>();
        }

        public async Task<RatingProfile> GetRatingProfile(int id, RatingQueryType ratingQueryType)
        {
            return await BaseRequest
                .AppendPathSegment("ratings")
                .AppendPathSegment(ratingQueryType.ToString().ToLower())
                .AppendPathSegment(id)
                .GetJsonAsync<RatingProfile>();
        }

        public async Task<List<RatingPeriod>> GetRatingPeriods(int page = 1)
        {
            var json = await BaseRequest
               .AppendPathSegment("rating-periods")
               .SetQueryParam("page", page)
               .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<RatingPeriod>>();
        }

        public async Task<SingleRatingPeriod> GetRatingPeriod(DateTime date)
        {
            return await BaseRequest
                           .AppendPathSegment("rating-periods")
                           .AppendPathSegment(date.ToString("yyyy-MM-dd"))
                           .GetJsonAsync<SingleRatingPeriod>();
        }

        #endregion
    }
}
