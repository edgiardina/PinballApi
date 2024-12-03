using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using PinballApi.Interfaces;
using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.Universal;
using PinballApi.Models.WPPR.Universal.Director;
using PinballApi.Models.WPPR.Universal.Directors;
using PinballApi.Models.WPPR.Universal.Players;
using PinballApi.Models.WPPR.Universal.Players.Search;
using PinballApi.Models.WPPR.Universal.Rankings;
using PinballApi.Models.WPPR.Universal.Rankings.Custom;
using PinballApi.Models.WPPR.Universal.Series;
using PinballApi.Models.WPPR.Universal.Stats;
using PinballApi.Models.WPPR.Universal.Tournaments;
using PinballApi.Models.WPPR.Universal.Tournaments.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PinballApi
{
    public class PinballRankingApi : BasePinballRankingApi, IPinballRankingApi
    {
        public PinballRankingApi(string apiKey) : base(apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API Key must be provided");
        }

        protected override IFlurlRequest BaseRequest => $"https://api.ifpapinball.com/"
                              .SetQueryParams(new { api_key = ApiKey })
                              .WithSettings(settings =>
                              {
                                  settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions);
                              });

        protected override PinballRankingApiVersion ApiVersion => PinballRankingApiVersion.Universal;

        #region Tournaments

        public async Task<TournamentSearch> TournamentSearch(double? latitude = null, double? longitude = null, int? radius = null, DistanceType? distanceType = null, string name = null, string country = null, string stateprov = null, DateTime? startDate = null, DateTime? endDate = null, TournamentType? tournamentType = null, int? startPosition = null,
            int? totalReturn = null, TournamentSearchSortMode? tournamentSearchSortMode = null, TournamentSearchSortOrder? tournamentSearchSortOrder = null, string directorName = null,
            bool? preRegistration = null, bool? onlyWithResults = null, double? minimumPoints = null, double? maximumPoints = null, bool? pointFilter = null, TournamentEventType? tournamentEventType = null)
        {

            var request = BaseRequest
                .AppendPathSegment("tournament/search");

            if (distanceType == DistanceType.Kilometers)
                request = request.SetQueryParam("k", radius);
            else if (distanceType == DistanceType.Miles)
                request = request.SetQueryParam("m", radius);

            if (latitude != 0 && longitude != 0)
            {
                request = request.SetQueryParam("latitude", latitude);
                request = request.SetQueryParam("longitude", longitude);
            }

            if (!string.IsNullOrEmpty(name))
                request = request.SetQueryParam("name", name);

            if (!string.IsNullOrEmpty(country))
                request = request.SetQueryParam("country", country);

            if (!string.IsNullOrEmpty(stateprov))
                request = request.SetQueryParam("stateprov", stateprov);

            if (startDate.HasValue)
                request = request.SetQueryParam("start_date", startDate.Value.ToString("yyyy-MM-dd"));

            if (endDate.HasValue)
                request = request.SetQueryParam("end_date", endDate.Value.ToString("yyyy-MM-dd"));

            if (tournamentType.HasValue)
            {
                //Tournament type must be MAIN or WOMEN
                if (tournamentType != TournamentType.Main && tournamentType != TournamentType.Women)
                    throw new ArgumentException("Tournament Type must be MAIN or WOMEN");

                request = request.SetQueryParam("rank_type", tournamentType.Value.ToString().ToUpper());
            }

            if (radius.HasValue)
                request = request.SetQueryParam("radius", radius);

            if (startPosition.HasValue)
                request = request.SetQueryParam("start_pos", startPosition);

            if (totalReturn.HasValue)
                request = request.SetQueryParam("total", totalReturn);

            if (tournamentSearchSortMode.HasValue)
                request = request.SetQueryParam("sort_mode", tournamentSearchSortMode.Value.ToString().ToUpper());

            if (tournamentSearchSortOrder.HasValue)
            {
                if (tournamentSearchSortOrder == TournamentSearchSortOrder.Ascending)
                    request = request.SetQueryParam("sort_order", "ASC");
                else
                    request = request.SetQueryParam("sort_order", "DESC");
            }

            if (minimumPoints.HasValue)
                request = request.SetQueryParam("min_points", minimumPoints);

            if (maximumPoints.HasValue)
                request = request.SetQueryParam("max_points", maximumPoints);

            if (pointFilter.HasValue)
            {
                if (pointFilter == true)
                    request = request.SetQueryParam("point_filter", "Y");
                else
                    request = request.SetQueryParam("point_filter", "N");
            }

            if (preRegistration.HasValue)
            {
                if (preRegistration == true)
                    request = request.SetQueryParam("preregistration", "Y");
                else
                    request = request.SetQueryParam("preregistration", "N");
            }

            if (onlyWithResults.HasValue)
            {
                if (onlyWithResults == true)
                    request = request.SetQueryParam("only_with_results", "Y");
                else
                    request = request.SetQueryParam("only_with_results", "N");
            }

            if (!string.IsNullOrEmpty(directorName))
                request = request.SetQueryParam("director_name", directorName);

            if (tournamentEventType.HasValue)
                request = request.SetQueryParam("event_type", tournamentEventType.Value.ToString().ToUpper());

            return await request.GetJsonAsync<TournamentSearch>();
        }

        public async Task<Models.WPPR.Universal.Tournaments.Tournament> GetTournament(int tournamentId)
        {
            var request = BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId);

            return await request.GetJsonAsync<Models.WPPR.Universal.Tournaments.Tournament>();
        }

        public async Task<TournamentFormats> GetTournamentFormats()
        {
            return await BaseRequest
                .AppendPathSegment("tournament/formats")
                .GetJsonAsync<TournamentFormats>();
        }

        public async Task<TournamentResults> GetTournamentResults(int tournamentId)
        {
            return await BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("results")
                .GetJsonAsync<TournamentResults>();
        }

        // Get Related Tournaments
        public async Task<List<TournamentResult>> GetRelatedResults(int tournamentId)
        {
            throw new NotImplementedException("Endpoint currently returns 404");

            var request = BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("related");

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["results"].Deserialize<List<TournamentResult>>(JsonSerializerOptions);
        }

        // Get Leagues
        public async Task<List<League>> GetLeagues(LeagueTimePeriod timePeriod)
        {
            throw new NotImplementedException("Endpoint currently returns 404");

            var json = await BaseRequest
                .AppendPathSegment("tournament/leagues")
                .AppendPathSegment(timePeriod.ToString().ToLower())
                .GetStringAsync();

            return JsonNode.Parse(json)["results"].Deserialize<List<League>>(JsonSerializerOptions);
        }

        #endregion

        #region Rankings

        public async Task<RankingCountries> GetRankingCountries()
        {
            var request = BaseRequest
                .AppendPathSegment("rankings/country_list");

            return await request.GetJsonAsync<RankingCountries>();
        }

        public async Task<RankingSearch> RankingSearch(RankingType rankingType, RankingSystem rankingSystem = RankingSystem.Open, int count = 100, int startPosition = 1, string countryCode = null)
        {
            if (rankingType == RankingType.Pro)
                throw new ArgumentException("Use Pro Ranking Search method for Pro Rankings");

            if (rankingType == RankingType.Country && string.IsNullOrEmpty(countryCode))
                throw new ArgumentException("Country Code must be provided for Country Rankings");

            var request = BaseRequest
                .AppendPathSegment("rankings")
                .AppendPathSegment(rankingType.ToString().ToLower())
                .SetQueryParams(new
                {
                    start_pos = startPosition,
                    count
                });

            if (rankingType == RankingType.Women)
            {
                request = request.AppendPathSegment(rankingSystem.ToString().ToLower());
            }
            else if (rankingType == RankingType.Country)
            {
                request = request.SetQueryParam("country", countryCode);
            }

            return await request.GetJsonAsync<RankingSearch>();
        }

        public async Task<ProRankingSearch> ProRankingSearch(TournamentType rankingSystem = TournamentType.Open)
        {
            if (rankingSystem == TournamentType.Youth)
                throw new ArgumentException("Youth Pro Rankings are not supported");

            if (rankingSystem == TournamentType.Main)
                rankingSystem = TournamentType.Open;

            var request = BaseRequest
                            .AppendPathSegment("rankings")
                            .AppendPathSegment("pro")
                            .AppendPathSegment(rankingSystem.ToString().ToLower());

            return await request.GetJsonAsync<ProRankingSearch>();
        }

        public async Task<List<CustomRankingView>> GetCustomRankings()
        {
            var json = await BaseRequest
                .AppendPathSegment("rankings/custom/list")
                .GetStringAsync();

            return JsonNode.Parse(json)["custom_view"].Deserialize<List<CustomRankingView>>(JsonSerializerOptions);
        }

        public async Task<CustomRankingViewResult> GetCustomRankingViewResult(int viewId, int count = 50)
        {
            if(count > 500)
                throw new ArgumentException("Count must be 500 or less");

            if (count < 1)
                throw new ArgumentException("Count must be 1 or more");

            return await BaseRequest
                .AppendPathSegment("rankings/custom")
                .AppendPathSegment(viewId)
                .SetQueryParam("count", count)
                .GetJsonAsync<CustomRankingViewResult>();
        }

        #endregion

        #region Players

        /// <summary>
        /// Get individual player record
        /// </summary>
        /// <param name="playerId">Player to examine</param>
        /// <returns></returns>
        public async Task<Player> GetPlayer(int playerId)
        {
            var json = await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .GetStringAsync();

            return JsonNode.Parse(json)["player"].Deserialize<List<Player>>(JsonSerializerOptions).First();
        }

        /// <summary>
        /// Get Multiple player records
        /// </summary>
        /// <param name="playerIds">list of Player Ids to return</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<Player>> GetPlayers(List<int> playerIds)
        {
            if (playerIds.Count > 50)
                throw new ArgumentException("GetPlayers can only process 50 or less player IDs at a time due to limitations in the IFPA API.");

            var request = BaseRequest
                .AppendPathSegment("player");

            request = request.SetQueryParam("players", string.Join(",", playerIds));

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["player"].Deserialize<List<Player>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Find player by name and / or country
        /// </summary>
        /// <param name="name">Name of the player (not case sensitive)</param>
        /// <param name="country">Country Name or 2-digit code</param>
        /// <param name="stateProv">2 digit State or Providence Code</param>
        /// <param name="tournamentName">Name of tournament that a player may have played in. Partial Strings are accepted</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<PlayerSearch> PlayerSearch(string name = null, string country = null, string stateProv = null, string tournamentName = null)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(country) && string.IsNullOrWhiteSpace(stateProv) && string.IsNullOrWhiteSpace(tournamentName))
                throw new ArgumentException("Name or Country or State/Province or Tournament Name must be provided");

            var request = BaseRequest
                .AppendPathSegment("player/search");

            if (!string.IsNullOrEmpty(name))
                request = request
                .SetQueryParam("name", name);

            if (!string.IsNullOrEmpty(country))
                request = request
                .SetQueryParam("country", country);

            if (!string.IsNullOrEmpty(stateProv))
                request = request
                    .SetQueryParam("stateprov", stateProv);

            if (!string.IsNullOrEmpty(tournamentName))
                request = request
                    .SetQueryParam("tournament", tournamentName);

            return await request.GetJsonAsync<PlayerSearch>();
        }

        /// <summary>
        /// Get a player's tournament results
        /// </summary>
        /// <param name="playerId">Player to examine</param>
        /// <param name="rankingSystem">Player's Ranking System</param>
        /// <param name="resultType">Active, Noactive or Inactive classification of results</param>
        /// <returns></returns>
        public async Task<PlayerResults> GetPlayerResults(int playerId, PlayerRankingSystem rankingSystem = PlayerRankingSystem.Main, ResultType resultType = ResultType.Active)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .AppendPathSegment("results")
                    .AppendPathSegment(rankingSystem.ToString().ToLower())
                    .AppendPathSegment(resultType.ToString().ToLower())
                    .GetJsonAsync<PlayerResults>();
        }

        /// <summary>
        /// Get a player's history
        /// </summary>
        /// <param name="playerId">Player to examine</param>
        /// <param name="playerSystem">Player's ranking system</param>
        /// <param name="activeResultsOnly">Return only active results, or all results</param>
        /// <returns></returns>
        public async Task<PlayerHistory> GetPlayerHistory(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, bool activeResultsOnly = false)
        {
            var request = BaseRequest
                            .AppendPathSegment("player")
                            .AppendPathSegment(playerId)
                            .AppendPathSegment("rank_history");

            request = request.SetQueryParam("system", playerSystem.ToString().ToUpper());

            if (activeResultsOnly)
                request = request.SetQueryParam("active_flag", "Y");

            return await request.GetJsonAsync<PlayerHistory>();
        }

        /// <summary>
        /// Get a Player's Versus Player records
        /// </summary>
        /// <param name="playerId">Player to examine</param>
        /// <param name="playerSystem">Player's ranking system</param>
        /// <returns></returns>
        public async Task<PlayerVersusPlayer> GetPlayerVersusPlayer(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            var request = BaseRequest
                            .AppendPathSegment("player")
                            .AppendPathSegment(playerId)
                            .AppendPathSegment("pvp");

            request = request.SetQueryParam("system", playerSystem.ToString().ToUpper());

            return await request.GetJsonAsync<PlayerVersusPlayer>();
        }

        /// <summary>
        /// Get a Player Versus Player comparison to a specific player
        /// </summary>
        /// <param name="playerId">Player One to Compare</param>
        /// <param name="comparisonPlayerId">Player Two to Compare</param>
        /// <returns></returns>
        public async Task<PlayerVersusPlayerComparison> GetPlayerVersusPlayerComparison(int playerId, int comparisonPlayerId)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .AppendPathSegment("pvp")
                    .AppendPathSegment(comparisonPlayerId)
                    .GetJsonAsync<PlayerVersusPlayerComparison>();
        }

        #endregion

        #region Series

        public async Task<List<Series>> GetSeries()
        {
            var json = await BaseRequest
                  .AppendPathSegment("series/list")
                  .GetStringAsync();

            return JsonNode.Parse(json)["series"].Deserialize<List<Series>>(JsonSerializerOptions);
        }

        /// <summary>
        /// Retrieve overall standings for a Series
        /// </summary>
        /// <param name="seriesCode">Series abbreviation (e.g. NACS)</param>
        /// <param name="year">Optional year (current year is default)</param>
        /// <returns></returns>
        public async Task<SeriesOverallResults> GetSeriesOverallStanding(string seriesCode, int? year = null)
        {
            var request = BaseRequest
                .AppendPathSegment("series")
                .AppendPathSegment(seriesCode.ToUpper())
                .AppendPathSegment("overall_standings");

            if (year.HasValue)
            {
                request.SetQueryParam("year", year.Value);
            }

            return await request.GetJsonAsync<SeriesOverallResults>();
        }

        /// <summary>
        /// Get Series Standings for Region
        /// </summary>
        /// <param name="seriesCode">Series abbreviation (e.g. NACS)</param>
        /// <param name="region">Abbreviation for region (e.g. MA for Massachusetts)</param>
        /// <param name="year">Optional year (current year is default)</param>
        /// <returns></returns>
        public async Task<RegionStandings> GetSeriesStandingsForRegion(string seriesCode, string region, int? year = null)
        {
            var request = BaseRequest
                .AppendPathSegment("series")
                .AppendPathSegment(seriesCode.ToUpper())
                .AppendPathSegment("standings")
                .SetQueryParam("region_code", region);

            if (year.HasValue)
            {
                request.SetQueryParam("year", year.Value);
            }

            return await request.GetJsonAsync<RegionStandings>();
        }

        /// <summary>
        /// Get Tournaments in Series for Region
        /// </summary>
        /// <param name="seriesCode">Series abbreviation (e.g. NACS)</param>
        /// <param name="region">Abbreviation for region (e.g. MA for Massachusetts)</param>
        /// <param name="year">Optional year (current year is default)</param>
        /// <returns></returns>
        public async Task<SeriesTournaments> GetSeriesTournamentsForRegion(string seriesCode, string region, int? year = null)
        {
            var request = BaseRequest
                .AppendPathSegment("series")
                .AppendPathSegment(seriesCode.ToUpper())
                .AppendPathSegment("tournaments")
                .SetQueryParam("region_code", region);

            if (year.HasValue)
            {
                request.SetQueryParam("year", year.Value);
            }

            return await request.GetJsonAsync<SeriesTournaments>();
        }

        /// <summary>
        /// Get a player's top 20 results for a region within a series
        /// </summary>
        /// <param name="playerId">Player's id</param>
        /// <param name="seriesCode">Series abbreviation (e.g. NACS)</param>
        /// <param name="region">Abbreviation for region (e.g. MA for Massachusetts)</param>
        /// <param name="year">Optional year (current year is default)</param>
        /// <returns></returns>
        public async Task<SeriesPlayerCard> GetSeriesPlayerCard(int playerId, string seriesCode, string region, int? year = null)
        {
            var request = BaseRequest
                .AppendPathSegment("series")
                .AppendPathSegment(seriesCode.ToUpper())
                .AppendPathSegment("player_card")
                .AppendPathSegment(playerId)
                .SetQueryParam("region_code", region);

            if (year.HasValue)
            {
                request.SetQueryParam("year", year.Value);
            }

            return await request.GetJsonAsync<SeriesPlayerCard>();
        }

        /// <summary>
        /// Retrieve the winners for past series
        /// </summary>
        /// <param name="seriesCode">Series abbreviation (e.g. NACS)</param>
        /// <param name="region">Optional abbreviation for region (e.g. MA for Massachusetts)</param>
        /// <returns></returns>
        public async Task<SeriesWinners> GetSeriesWinners(string seriesCode, string region = null)
        {
            var request = BaseRequest
                .AppendPathSegment("series")
                .AppendPathSegment(seriesCode.ToUpper())
                .AppendPathSegment("past_winners");

            if (!string.IsNullOrEmpty(region))
            {
                request.SetQueryParam("region_code", region);
            }

            return await request.GetJsonAsync<SeriesWinners>();
        }

        #endregion

        #region Directors

        public async Task<Director> GetDirector(long directorId)
        {
            return await BaseRequest
                    .AppendPathSegment("director")
                    .AppendPathSegment(directorId)
                    .GetJsonAsync<Director>();
        }

        public async Task<List<CountryDirector>> GetCountryDirectors()
        {
            var json = await BaseRequest
                    .AppendPathSegment("director/country")
                    .GetStringAsync();

            return JsonNode.Parse(json)["country_directors"].Deserialize<List<CountryDirector>>(JsonSerializerOptions);
        }

        public async Task<List<Director>> GetDirectorsBySearch(string name, int count = 50)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name must be provided");

            var json =  await BaseRequest
                    .AppendPathSegment("director/search")
                    .SetQueryParam("name", name)
                    .SetQueryParam("count", count)
                    .GetStringAsync();

            return JsonNode.Parse(json)["directors"].Deserialize<List<Director>>(JsonSerializerOptions);
        }


        public async Task<Models.WPPR.Universal.Tournaments.Tournament> GetDirectorTournaments(long directorId, TimePeriod timePeriod)
        {
            throw new NotImplementedException();

            // TODO: Implement - Currently throws 404
            /*
             curl -X 'GET' \
                  'https://api.ifpapinball.com/director/3357/tournaments/FUTURE' \
                  -H 'accept: application/json' \
                  -H 'X-API-Key: *****'
             */

            //var request = BaseRequest
            //    .AppendPathSegment("director")
            //    .AppendPathSegment(directorId)
            //    .AppendPathSegment("tournaments")
            //    .AppendPathSegment(eventType.ToString().ToUpper());

            //return await request.GetJsonAsync<List<DirectorTournament>>();
        }

        #endregion

        #region Stats
        public async Task<OverallStatistics> GetOverallStatistics()
        {
            var json = await BaseRequest
                    .AppendPathSegment("stats/overall")
                    .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<OverallStatistics>();
        }

        public async Task<List<EventsByYearStatistics>> GetEventsByYearStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest
                .AppendPathSegment("stats/events_by_year");

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<EventsByYearStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<LargestTournamentStatistics>> GetLargestTournamentStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest.AppendPathSegment("stats/largest_tournaments");

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<LargestTournamentStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<LucrativeTournamentStatistics>> GetLucrativeTournamentStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest.AppendPathSegment("stats/lucrative_tournaments");

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<LucrativeTournamentStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByYearStatistics>> GetPlayersByYearStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/players_by_year")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByYearStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByStateStatistics>> GetPlayersByStateStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest
                .AppendPathSegment("stats/state_players");

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByStateStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<TournamentsByStateStatistics>> GetTournamentsByStateStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest
                .AppendPathSegment("stats/state_tournaments");

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<TournamentsByStateStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByCountryStatistics>> GetPlayersByCountryStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest
                .AppendPathSegment("stats/country_players");

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByCountryStatistics>>(JsonSerializerOptions);
        }

        ///stats/points_given_period
        public async Task<List<PlayersPointsByGivenPeriodStatistics>> GetPlayersPointsByGivenPeriod(DateOnly startDate, DateOnly endDate, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, int limit = 25)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest
                .AppendPathSegment("stats/points_given_period")
                .SetQueryParam("start_date", startDate.ToString("yyyy-MM-dd"))
                .SetQueryParam("end_date", endDate.ToString("yyyy-MM-dd"))
                .SetQueryParam("limit", limit);

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersPointsByGivenPeriodStatistics>>(JsonSerializerOptions);
        }

        ////stats/events_attended_period
        public async Task<List<PlayersEventsAttendedByGivenPeriodStatistics>> GetPlayersEventsAttendedByGivenPeriod(DateOnly startDate, DateOnly endDate, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, int limit = 25)
        {
            if (playerSystem == PlayerRankingSystem.Youth)
                throw new ArgumentException("Youth Rankings are not supported");

            var request = BaseRequest
                .AppendPathSegment("stats/events_attended_period")
                .SetQueryParam("start_date", startDate.ToString("yyyy-MM-dd"))
                .SetQueryParam("end_date", endDate.ToString("yyyy-MM-dd"))
                .SetQueryParam("limit", limit);

            if (playerSystem != PlayerRankingSystem.Main)
                request = request.SetQueryParam("rank_type", playerSystem.ToString().ToUpper());

            var json = await request.GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersEventsAttendedByGivenPeriodStatistics>>(JsonSerializerOptions);
        }

        #endregion
    }
}
