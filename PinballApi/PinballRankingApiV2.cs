using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.v2.Players;
using System.Linq;
using PinballApi.Models.WPPR.v2.Nacs;
using PinballApi.Models.WPPR.v2;
using PinballApi.Models.WPPR.v2.Rankings;
using PinballApi.Models.WPPR.v2.Tournaments;
using PinballApi.Models.WPPR.v2.Directors;
using PinballApi.Models.WPPR.v2.Stats;
using PinballApi.Models.WPPR.v2.Series;

namespace PinballApi
{
    public class PinballRankingApiV2 : BasePinballRankingApi
    {
        protected override PinballRankingApiVersion ApiVersion => PinballRankingApiVersion.v2;

        public PinballRankingApiV2(string apiKey) : base(apiKey)
        {

        }

        #region player

        public async Task<Player> GetPlayer(int playerId)
        {
            try
            {
                var json = await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .GetStringAsync();

                return JObject.Parse(json)
                    .SelectToken("player", false).ToObject<List<Player>>().First();
            }
            catch (FlurlHttpException ex) when (ex.InnerException is JsonSerializationException)
            {
                //Indicates null values which may mean invalid playerId
                return null;
            }
        }

        public async Task<List<Player>> GetPlayers(List<int> playerIds)
        {
            if (playerIds.Count > 50)
                throw new ArgumentException("GetPlayers can only process 50 or less player IDs at a time due to limitations in the IFPA API.");

            var request = BaseRequest
                .AppendPathSegment("player");

            request = request.SetQueryParam("players", string.Join(",", playerIds));

            var json = await request.GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("player", false).ToObject<List<Player>>();
        }

        public async Task<PlayerResults> GetPlayerResults(int playerId, RankingType rankingType = RankingType.Main, ResultType resultType = ResultType.Active)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .AppendPathSegment("results")
                    .AppendPathSegment(rankingType.ToString().ToLower())
                    .AppendPathSegment(resultType.ToString().ToLower())
                    .GetJsonAsync<PlayerResults>();
        }

        public async Task<PlayerHistory> GetPlayerHistory(int playerId)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .AppendPathSegment("rank_history")
                    .GetJsonAsync<PlayerHistory>();
        }

        public async Task<PlayerVersusPlayer> GetPlayerVersusPlayer(int playerId)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .AppendPathSegment("pvp")
                    .GetJsonAsync<PlayerVersusPlayer>();
        }

        public async Task<ElitePlayerVersusPlayer> GetPlayerVersusElitePlayer(int elitePlayerId)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(elitePlayerId)
                    .AppendPathSegment("pvp/elite")
                    .GetJsonAsync<ElitePlayerVersusPlayer>();
        }

        public async Task<PlayerVersusPlayerComparison> GetPlayerVersusPlayerComparison(int playerId, int comparisonPlayerId)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .AppendPathSegment("pvp")
                    .AppendPathSegment(comparisonPlayerId)
                    .GetJsonAsync<PlayerVersusPlayerComparison>();
        }

        //name, country, stateprov, tournament, tourpos
        public async Task<PlayerSearch> GetPlayersBySearch(PlayerSearchFilter searchFilter)
        {
            if (searchFilter == null)
                throw new ArgumentNullException(nameof(searchFilter));

            var request = BaseRequest
                    .AppendPathSegment("player/search");

            if (!string.IsNullOrEmpty(searchFilter.Name))
            {
                request = request.SetQueryParam("name", searchFilter.Name);
            }

            if (!string.IsNullOrEmpty(searchFilter.Country))
            {
                request = request.SetQueryParam("country", searchFilter.Country);
            }

            if (!string.IsNullOrEmpty(searchFilter.StateProvince))
            {
                request = request.SetQueryParam("stateprov", searchFilter.StateProvince);
            }

            if (!string.IsNullOrEmpty(searchFilter.Tournament))
            {
                request = request.SetQueryParam("tournament", searchFilter.Tournament);
            }

            if (!string.IsNullOrEmpty(searchFilter.Tourpos))
            {
                request = request.SetQueryParam("tourpos", searchFilter.Tourpos);
            }

            if (request.QueryParams.Count == 1)
                throw new ArgumentOutOfRangeException("Expected at least one value in the provided searchFilter was not null or empty.");

            return await request.GetJsonAsync<PlayerSearch>();
        }

        #endregion

        #region Series

        public async Task<List<Series>> GetSeries()
        {
            var json = await BaseRequest
                  .AppendPathSegment("series/list")                  
                  .GetStringAsync();

            return JObject.Parse(json)
             .SelectToken("series", false).ToObject<List<Series>>();
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

            if(year.HasValue)
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

        #region NACS
        [Obsolete("Use Series functions instead of Nacs functions")]
        public async Task<List<NacsStandings>> GetNacsStandings(int year)
        {
            var json = await BaseRequest
                .AppendPathSegment("nacs/standings")
                .SetQueryParam("year", year)
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("standings", false).ToObject<List<NacsStandings>>();
        }
        [Obsolete("Use Series functions instead of Nacs functions")]
        public async Task<NacsStateProvinceStandings> GetNacsStateProvinceStandings(string stateProvinceAbbreviation, int year)
        {
            return await BaseRequest
                .AppendPathSegment("nacs/standings")            
                .AppendPathSegment(stateProvinceAbbreviation)
                .SetQueryParam("year", year)
                .GetJsonAsync<NacsStateProvinceStandings>();
        }
        [Obsolete("Use Series functions instead of Nacs functions")]
        public async Task<NacsStatisticsByYear> GetNacsStatistics(int year)
        {
            return await BaseRequest
                    .AppendPathSegment("nacs/stats")
                    .SetQueryParam("year", year)
                    .GetJsonAsync<NacsStatisticsByYear>();
        }
        [Obsolete("Use Series functions instead of Nacs functions")]
        public async Task<List<NacsPastWinners>> GetNacsPastWinners()
        {
            var json = await BaseRequest
                .AppendPathSegment("nacs/winners")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("results", false).ToObject<List<NacsPastWinners>>();
        }
        [Obsolete("Use Series functions instead of Nacs functions")]
        public async Task<NacsTournamentCard> GetNacsTournamentCard(int year, int playerId, string stateProvinceAbbreviation)
        {
            return await BaseRequest
                .AppendPathSegment("nacs/tournament_card")
                .SetQueryParams(new
                {
                    year,
                    player_id = playerId,
                    location_code = stateProvinceAbbreviation
                })
                .GetJsonAsync<NacsTournamentCard>();
        }

        #endregion       

        #region Rankings

        public async Task<CountryList> GetRankingCountries()
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/country_list")
                    .GetJsonAsync<CountryList>();
        }

        public async Task<CountryRanking> GetRankingForCountry(string countryName, int startPosition = 1, int count = 50)
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/country")
                    .SetQueryParam("country", countryName)
                    .SetQueryParams(new
                    {
                        start_pos = startPosition,
                        count
                    })
                    .GetJsonAsync<CountryRanking>();
        }

        public async Task<EliteRanking> GetEliteRanking(int startPosition = 1, int count = 50)
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/elite")
                    .SetQueryParams(new
                    {
                        start_pos = startPosition,
                        count
                    })
                    .GetJsonAsync<EliteRanking>();
        }

        public async Task<WomensRanking> GetRankingForWomen(TournamentType tournamentType, int startPosition = 1, int count = 50)
        {
            var request = BaseRequest
                    .AppendPathSegment("rankings/women");

            if (tournamentType == TournamentType.Women)
            {
                request = request.AppendPathSegment("restricted");
            }
            else
            {
                request = request.AppendPathSegment(tournamentType.ToString().ToLower());
            }

            request.SetQueryParams(new
            {
                start_pos = startPosition,
                count
            });

            return await request.GetJsonAsync<WomensRanking>();
        }

        public async Task<YouthRanking> GetRankingForYouth(int startPosition = 1, int count = 50)
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/youth")
                    .SetQueryParams(new
                    {
                        start_pos = startPosition,
                        count
                    })
                    .GetJsonAsync<YouthRanking>();
        }

        public async Task<WpprRanking> GetWpprRanking(int startPosition = 1, int count = 50)
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/wppr")
                    .SetQueryParams(new
                    {
                        start_pos = startPosition,
                        count
                    })
                    .GetJsonAsync<WpprRanking>();
        }

        public async Task<CustomRanking> GetRankingCustomView(int viewId)
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/custom")
                    .AppendPathSegment(viewId)
                    .GetJsonAsync<CustomRanking>();
        }

        public async Task<CustomRankingList> GetRankingCustomViewList()
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/custom/list")
                    .GetJsonAsync<CustomRankingList>();
        }


        #endregion

        #region Tournaments
        public async Task<Models.WPPR.v2.Tournaments.Tournament> GetTournament(int tournamentId)
        {
            var json = await BaseRequest
                    .AppendPathSegment("tournament")
                    .AppendPathSegment(tournamentId)
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("tournament", false).ToObject<Models.WPPR.v2.Tournaments.Tournament>();
        }

        public async Task<List<Models.WPPR.v2.Tournaments.Tournament>> GetRelatedTournaments(int tournamentId)
        {
            var json = await BaseRequest
                    .AppendPathSegment("tournament")
                    .AppendPathSegment(tournamentId)
                    .AppendPathSegment("related")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("tournament", false).ToObject<List<Models.WPPR.v2.Tournaments.Tournament>>();
        }

        public async Task<TournamentResults> GetTournamentResults(int tournamentId)
        {
            return await BaseRequest
                    .AppendPathSegment("tournament")
                    .AppendPathSegment(tournamentId)
                    .AppendPathSegment("results")
                    .GetJsonAsync<TournamentResults>();
        }

        public async Task<List<Models.WPPR.v2.Tournaments.TournamentWinner>> GetTournamentWinners(int tournamentId)
        {
            var json = await BaseRequest
                    .AppendPathSegment("tournament")
                    .AppendPathSegment(tournamentId)
                    .AppendPathSegment("winners/list")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("winners", false).ToObject<List<Models.WPPR.v2.Tournaments.TournamentWinner>>();
        }

        public async Task<List<TournamentWinnerGrouped>> GetTournamentWinnersGrouped(int tournamentId)
        {
            var json = await BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("winners/group")
                .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("winners", false).ToObject<List<TournamentWinnerGrouped>>();
        }

        public async Task<TournamentSearch> GetTournamentBySearch(TournamentSearchFilter searchFilter)
        {
            var request = BaseRequest
                    .AppendPathSegment("tournament")
                    .AppendPathSegment("search");

            if (!string.IsNullOrEmpty(searchFilter.Name))
            {
                request = request.SetQueryParam("name", searchFilter.Name);
            }

            if (!string.IsNullOrEmpty(searchFilter.Country))
            {
                request = request.SetQueryParam("country", searchFilter.Country);
            }

            if (!string.IsNullOrEmpty(searchFilter.StateProvince))
            {
                request = request.SetQueryParam("stateprov", searchFilter.StateProvince);
            }

            if (searchFilter.StartDate.HasValue)
            {
                request = request.SetQueryParam("start_date", searchFilter.StartDate.Value.ToString("yyyy-MM-dd"));
            }

            if (searchFilter.EndDate.HasValue)
            {
                request = request.SetQueryParam("end_date", searchFilter.EndDate.Value.ToString("yyyy-MM-dd"));
            }

            if (searchFilter.RankingType.HasValue)
            {
                if (searchFilter.RankingType.Value != RankingType.Main || searchFilter.RankingType.Value != RankingType.Women)
                {
                    throw new ArgumentOutOfRangeException("Tournament search only supports Main or Women's ranking");
                }

                request = request.SetQueryParam("rank_type", searchFilter.RankingType.Value.ToString());
            }

            if (request.QueryParams.Count == 1)
                throw new ArgumentOutOfRangeException("Expected at least one value in the provided searchFilter was not null or empty.");

            return await request.GetJsonAsync<TournamentSearch>();
        }


        #endregion

        #region Directors
        public async Task<List<Director>> GetNacsDirectors()
        {
            var json = await BaseRequest
                    .AppendPathSegment("director/nacs")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("nacs_directors", false).ToObject<List<Director>>();
        }

        public async Task<List<Director>> GetCountryDirectors()
        {
            var json = await BaseRequest
                    .AppendPathSegment("director/country")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("country_directors", false).ToObject<List<Director>>();
        }

        public async Task<List<Director>> GetDirectorList()
        {
            var json = await BaseRequest
                    .AppendPathSegment("director/list")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("country_directors", false).ToObject<List<Director>>();
        }

        public async Task<List<Director>> GetDirector(int directorId)
        {
            var json = await BaseRequest
                    .AppendPathSegment("director")
                    .AppendPathSegment(directorId)
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("country_directors", false).ToObject<List<Director>>();
        }

        #endregion

        #region Stats
        public async Task<OverallStatistics> GetOverallStatistics()
        {
            var json = await BaseRequest
                    .AppendPathSegment("stats/overall")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("stats", false).ToObject<OverallStatistics>();
        }

        public async Task<List<EventsByYearStatistics>> GetEventsByYearStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/events_by_year")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<EventsByYearStatistics>>();
        }

        public async Task<List<LargestTournamentStatistics>> GetLargestTournamentStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/largest_tournaments")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<LargestTournamentStatistics>>();
        }

        public async Task<List<LucrativeTournamentStatistics>> GetLucrativeTournamentStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/lucrative_tournaments")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<LucrativeTournamentStatistics>>();
        }

        public async Task<List<PlayersByYearStatistics>> GetPlayersByYearStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/players_by_year")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PlayersByYearStatistics>>();
        }

        public async Task<List<PlayersByStateStatistics>> GetPlayersByStateStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/state_players")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PlayersByStateStatistics>>();
        }

        public async Task<List<TournamentsByStateStatistics>> GetTournamentsByStateStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/state_tournaments")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<TournamentsByStateStatistics>>();
        }

        public async Task<List<PlayersByCountryStatistics>> GetPlayersByCountryStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/country_players")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PlayersByCountryStatistics>>();
        }

        ///stats/points_given_period
        public async Task<List<PlayersPointsByGivenPeriodStatistics>> GetPlayersPointsByGivenPeriod(DateTime startDate, DateTime endDate, int limit = 25)
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/points_given_period")
                .SetQueryParam("start_date", startDate)
                .SetQueryParam("end_date", endDate)
                .SetQueryParam("limit", limit)
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PlayersPointsByGivenPeriodStatistics>>();
        }

        ////stats/events_attended_period
        public async Task<List<PlayersEventsAttendedByGivenPeriodStatistics>> GetPlayersEventsAttendedByGivenPeriod(DateTime startDate, DateTime endDate, int limit = 25)
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/events_attended_period")
                .SetQueryParam("start_date", startDate)
                .SetQueryParam("end_date", endDate)
                .SetQueryParam("limit", limit)
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PlayersEventsAttendedByGivenPeriodStatistics>>();
        }

        #endregion

    }
}
