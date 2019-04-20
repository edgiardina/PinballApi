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
        public async Task<PlayerSearch> GetPlayersBySearch(Models.WPPR.v2.Players.SearchFilter searchFilter)
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
    }
}
