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
using PinballApi.Models.WPPR.v2.Calendar;
using PinballApi.Models.WPPR.v2;
using PinballApi.Models.WPPR.v2.Rankings;
using PinballApi.Models.WPPR.v2.Tournaments;
using PinballApi.Models.WPPR.v2.Directors;
using PinballApi.Models.WPPR.v2.Stats;
using PinballApi.Models.WPPR.v1.Statistics;

namespace PinballApi
{
    public class PinballRankingApiV2 : BasePinballRankingApi
    {
        protected override PinballRankingApiVersion ApiVersion => PinballRankingApiVersion.v2beta;

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
            var request = BaseRequest
                .AppendPathSegment("player");

            request = request.SetQueryParam("players", string.Join(",", playerIds));

            return await request.GetJsonAsync<List<Player>>();
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

        public async Task<Player> GetPlayerAcheivements(int playerId)
        {
            throw new NotImplementedException("Achievements 404s currently");

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

        public async Task<PlayerHistory> GetPlayerHistory(int playerId)
        {
            return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .AppendPathSegment("history")
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
        public async Task<PlayerSearch> GetPlayersBySearch(Models.WPPR.v2.Players.PlayerSearchFilter searchFilter)
        {
            if (searchFilter == null)
                throw new ArgumentNullException(nameof(searchFilter));

            var request = BaseRequest
                    .AppendPathSegment("player/search");

            if(!string.IsNullOrEmpty(searchFilter.Name))
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


        #region NACS
        public async Task<List<NacsStandings>> GetNacsStandings()
        {
            var json = await BaseRequest
                .AppendPathSegment("nacs/standings")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("nacs", false).ToObject<List<NacsStandings>>();
        }

        public async Task<NacsStateProvinceStandings> GetNacsStateProvinceStandings(string stateProvinceAbbreviation)
        {
            return await BaseRequest
                .AppendPathSegment("nacs/standings")
                 .AppendPathSegment(stateProvinceAbbreviation)
                .GetJsonAsync<NacsStateProvinceStandings>();
        }

        public async Task<NacsStatistics> GetNacsStatistics()
        {
            return await BaseRequest
                    .AppendPathSegment("nacs/stats")
                    .GetJsonAsync<NacsStatistics>();
        }

        public async Task<List<NacsPastWinners>> GetNacsPastWinners()
        {
            var json = await BaseRequest
                .AppendPathSegment("nacs/winners")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("winners", false).ToObject<List<NacsPastWinners>>();
        }

        #endregion

        #region Calendar

        /*
           /calendar/upcoming
        */

        public async Task<CalendarEntry> GetCalendarEntry(int tournamentId)
        {
            return await BaseRequest
                    .AppendPathSegment("calendar")
                    .AppendPathSegment(tournamentId)
                    .GetJsonAsync<CalendarEntry>();
        }

        public async Task<CalendarSearch> GetCalendarEntriesBySearch(CalendarSearchFilter searchFilter)
        {
            return await BaseRequest
                    .AppendPathSegment("calendar/search")
                    .SetQueryParam("start_date", searchFilter.StartDate.ToString("yyyy-MM-dd"))
                    .SetQueryParam("end_date", searchFilter.EndDate.ToString("yyyy-MM-dd"))
                    .SetQueryParam("ranking_type", searchFilter.RankingType)
                    .SetQueryParam("stateprov", searchFilter.StateProvince)
                    .SetQueryParam("name", searchFilter.Name)
                    .GetJsonAsync<CalendarSearch>();
        }

        public async Task<CalendarDistanceSearch> GetCalendarEntriesByDistance(CalendarDistanceSearchFilter searchFilter)
        {
            return await BaseRequest
                    .AppendPathSegment("calendar/search/distance")
                    .SetQueryParam("address", searchFilter.Address)
                    .SetQueryParam(searchFilter.DistanceType.ToString().ToLower().Substring(0, 1), searchFilter.Distance)
                    .GetJsonAsync<CalendarDistanceSearch>();
        }


        public async Task<CalendarDistanceSearch> GetCalendarUpcoming()
        {
            throw new NotImplementedException("whoops");
        }

        #endregion

        #region Rankings

        public async Task<CountryList> GetRankingCountries()
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/country_list")     
                    .GetJsonAsync<CountryList>();
        }

        public async Task<CountryRanking> GetRankingForCountry(string countryName)
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/country")
                    .SetQueryParam("country", countryName)
                    .GetJsonAsync<CountryRanking>();
        }

        public async Task<EliteRanking> GetEliteRanking()
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/elite/list")
                    .GetJsonAsync<EliteRanking>();
        }

        public async Task<ElitePlayerVersusPlayer> GetElitePlayerVersusPlayer(int elitePlayerId)
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/elite/pvp")
                    .AppendPathSegment(elitePlayerId)
                    .GetJsonAsync<ElitePlayerVersusPlayer>();
        }

        public async Task<WomensRanking> GetRankingForWomen(TournamentType tournamentType)
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

            return await request.GetJsonAsync<WomensRanking>();
        }

        public async Task<YouthRanking> GetRankingForYouth()
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/youth")
                    .GetJsonAsync<YouthRanking>();
        }

        public async Task<WpprRanking> GetWpprRanking()
        {
            return await BaseRequest
                    .AppendPathSegment("rankings/wppr")
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

        public async Task<TournamentResults> GetTournamentResults(int tournamentId)
        {
            return await BaseRequest
                    .AppendPathSegment("tournament")
                    .AppendPathSegment(tournamentId)
                    .AppendPathSegment("results")
                    .GetJsonAsync<TournamentResults>();
        }

        public async Task<ActiveLeagues> GetActiveLeagues()
        {
            return await BaseRequest
                    .AppendPathSegment("tournament/leagues/active")      
                    .GetJsonAsync<ActiveLeagues>();
        }

        public async Task<HistoricalLeagues> GetHistoricalLeagues()
        {
            return await BaseRequest
                    .AppendPathSegment("tournament/leagues/history")
                    .GetJsonAsync<HistoricalLeagues>();
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

            if (request.QueryParams.Count == 1)
                throw new ArgumentOutOfRangeException("Expected at least one value in the provided searchFilter was not null or empty.");

            return await request.GetJsonAsync<TournamentSearch>();
        }


        #endregion

        #region Directors
        public async Task<List<Director>> GetNacsDirectors()
        {
            var json = await BaseRequest
                    .AppendPathSegment("directors/nacs")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("nacs_directors", false).ToObject<List<Director>>();
        }

        public async Task<List<Director>> GetCountryDirectors()
        {
            var json = await BaseRequest
                    .AppendPathSegment("directors/country")
                    .GetStringAsync();

            return JObject.Parse(json)
            .SelectToken("country_director", false).ToObject<List<Director>>();
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


        #endregion

    }
}
