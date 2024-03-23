using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.v2.Players;
using System.Linq;
using PinballApi.Models.WPPR.v2;
using PinballApi.Models.WPPR.v2.Rankings;
using PinballApi.Models.WPPR.v2.Tournaments;
using PinballApi.Models.WPPR.v2.Directors;
using PinballApi.Models.WPPR.v2.Stats;
using PinballApi.Models.WPPR.v2.Series;
using PinballApi.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;
using System.Text.Json;
using Flurl;

namespace PinballApi
{
    public class PinballRankingApiV2 : BasePinballRankingApi, IPinballRankingApiV2
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

                return JsonNode.Parse(json)["player"].Deserialize<List<Player>>(JsonSerializerOptions).First();
            }
            catch (FlurlHttpException ex) when (ex.InnerException is JsonException)
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

            return JsonNode.Parse(json)["player"].Deserialize<List<Player>>(JsonSerializerOptions);
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

            if (request.Url.QueryParams.Count == 1)
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

            return JsonNode.Parse(json)["tournament"].Deserialize<Models.WPPR.v2.Tournaments.Tournament>(JsonSerializerOptions);
        }

        public async Task<List<Models.WPPR.v2.Tournaments.Tournament>> GetRelatedTournaments(int tournamentId)
        {
            var json = await BaseRequest
                    .AppendPathSegment("tournament")
                    .AppendPathSegment(tournamentId)
                    .AppendPathSegment("related")
                    .GetStringAsync();

            return JsonNode.Parse(json)["tournament"].Deserialize<List<Models.WPPR.v2.Tournaments.Tournament>>(JsonSerializerOptions);
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

            return JsonNode.Parse(json)["winners"].Deserialize<List<Models.WPPR.v2.Tournaments.TournamentWinner>>(JsonSerializerOptions);
        }

        public async Task<List<TournamentWinnerGrouped>> GetTournamentWinnersGrouped(int tournamentId)
        {
            var json = await BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("winners/group")
                .GetStringAsync();

            return JsonNode.Parse(json)["winners"].Deserialize<List<TournamentWinnerGrouped>>(JsonSerializerOptions);
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

            if (request.Url.QueryParams.Count == 1)
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

            return JsonNode.Parse(json)["nacs_directors"].Deserialize<List<Director>>(JsonSerializerOptions);
        }

        public async Task<List<Director>> GetCountryDirectors()
        {
            var json = await BaseRequest
                    .AppendPathSegment("director/country")
                    .GetStringAsync();

            return JsonNode.Parse(json)["country_directors"].Deserialize<List<Director>>(JsonSerializerOptions);
        }

        public async Task<List<Director>> GetDirectorList()
        {
            var json = await BaseRequest
                    .AppendPathSegment("director/list")
                    .GetStringAsync();

            return JsonNode.Parse(json)["country_directors"].Deserialize<List<Director>>(JsonSerializerOptions);
        }

        public async Task<List<Director>> GetDirector(int directorId)
        {
            var json = await BaseRequest
                    .AppendPathSegment("director")
                    .AppendPathSegment(directorId)
                    .GetStringAsync();

            return JsonNode.Parse(json)["country_directors"].Deserialize<List<Director>>(JsonSerializerOptions);
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

        public async Task<List<EventsByYearStatistics>> GetEventsByYearStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/events_by_year")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<EventsByYearStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<LargestTournamentStatistics>> GetLargestTournamentStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/largest_tournaments")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<LargestTournamentStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<LucrativeTournamentStatistics>> GetLucrativeTournamentStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/lucrative_tournaments")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<LucrativeTournamentStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByYearStatistics>> GetPlayersByYearStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/players_by_year")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByYearStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByStateStatistics>> GetPlayersByStateStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/state_players")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByStateStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<TournamentsByStateStatistics>> GetTournamentsByStateStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/state_tournaments")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<TournamentsByStateStatistics>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByCountryStatistics>> GetPlayersByCountryStatistics()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats/country_players")
                .GetStringAsync();

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByCountryStatistics>>(JsonSerializerOptions);
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

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersPointsByGivenPeriodStatistics>>(JsonSerializerOptions);
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

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersEventsAttendedByGivenPeriodStatistics>>(JsonSerializerOptions);
        }

        #endregion

    }
}
