using PinballApi.Models.WPPR.v1.Calendar;
using PinballApi.Models.WPPR.v1.Players;
using PinballApi.Models.WPPR.v1.Pvp;
using PinballApi.Models.WPPR.v1.Rankings;
using PinballApi.Models.WPPR.v1.Statistics;
using PinballApi.Models.WPPR.v1.Tournaments;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PinballApi.Models.WPPR;

namespace PinballApi
{
    public class PinballRankingApiV1 : BasePinballRankingApi
    {
        protected override PinballRankingApiVersion ApiVersion => PinballRankingApiVersion.v1;

        public PinballRankingApiV1(string apiKey) : base(apiKey)
        {
                  
        }

        #region player

        public async Task<PlayerRecord> GetPlayerRecord(int playerId)
        {
            try
            {
                return
                 await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .GetJsonAsync<PlayerRecord>();
            }
            catch (FlurlHttpException ex) when (ex.InnerException is JsonSerializationException)
            {
                //Indicates null values which may mean invalid playerId
                return null;
            }
        }

        public async Task<PlayerResult> GetPlayerResults(int playerId)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .AppendPathSegment("results")
                .GetJsonAsync<PlayerResult>();
        }

        public async Task<PlayerComparisons> GetPlayerComparisons(int playerId)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .AppendPathSegment("pvp")
                .GetJsonAsync<PlayerComparisons>();
        }

        public async Task<PlayerSearch> SearchForPlayerByName(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be null");

            try
            {
                return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment("search")
                    .SetQueryParam("q", name)
                    .GetJsonAsync<PlayerSearch>();
            }
            catch (FlurlParsingException ex)
            {
                //admittedly this is a bit hacky. Might be better to have a custom converted on PlayerSearch's Search list but that gets tricky.
                if (ex.InnerException.Message.Contains("No players found"))
                {
                    return new PlayerSearch
                    {
                        Query = name,
                        Search = new List<Search>()
                    };
                }

                throw ex;
            }
        }

        public async Task<PlayerSearch> SearchForPlayerByEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                throw new ArgumentNullException("Email cannot be null");
            try
            {
                return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment("search")
                .SetQueryParam("email", email)
                .GetJsonAsync<PlayerSearch>();
            }
            catch (FlurlParsingException ex)
            {
                //admittedly this is a bit hacky. Might be better to have a custom converted on PlayerSearch's Search list but that gets tricky.
                if (ex.InnerException.Message.Contains("No players found"))
                {
                    return new PlayerSearch
                    {
                        Query = email,
                        Search = new List<Search>()
                    };
                }

                throw ex;
            }
        }

        public async Task<PlayerSearch> GetCountryDirectors()
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment("country_directors")
                .GetJsonAsync<PlayerSearch>();
        }

        public async Task<PlayerHistory> GetPlayerHistory(int playerId)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .AppendPathSegment("history")
                .GetJsonAsync<PlayerHistory>();
        }

        #endregion

        #region tournament

        public async Task<Tournament> GetTournament(int tournamentId)
        {
            var json = await BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("tournament", false).ToObject<Tournament>();
        }

        public async Task<TournamentResult> GetTournamentResults(int tournamentId, int? eventId = null, DateTime? tournamentDate = null)
        {
            var request = BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("results");

            if (eventId.HasValue)
                request = request.SetQueryParam("event_id", eventId.Value);

            if (tournamentDate.HasValue)
                request = request.SetQueryParam("tour_date", tournamentDate.Value.ToString("yyyy-MM-dd"));

            var json = await request.GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("tournament", false).ToObject<TournamentResult>();
        }

        public async Task<TournamentList> GetTournamentList(int startPosition = 1, int count = 50)
        {
            if (count > 250 || count < 1)
                throw new ArgumentException("Count must be less than or equal to 250", "count");

            if (startPosition < 1)
                throw new ArgumentException("Start Positon must be a positive integer", "startPosition");

            var request = BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment("list");

            if (startPosition > 1)
                request.SetQueryParam("start_pos", startPosition);

            if (count != 50)
                request.SetQueryParam("count", count);

            return await request.GetJsonAsync<TournamentList>();
        }

        public async Task<TournamentSearch> TournamentSearch(string tournamentName)
        {
            if (string.IsNullOrEmpty(tournamentName))
                throw new ArgumentNullException("Tournament Name cannot be null or empty");

            return await BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment("search")
                .SetQueryParam("q", tournamentName)
                .GetJsonAsync<TournamentSearch>();
        }

        #endregion

        #region ranking

        public async Task<RankingList> GetRankings(int startPosition = 1, int count = 50, RankingOrder order = RankingOrder.points, string countryName = null)
        {
            return await BaseRequest
                .AppendPathSegment("rankings")
                .SetQueryParams(new
                {
                    start_pos = startPosition,
                    count = count,
                    order = order,
                    country = countryName
                })
                .GetJsonAsync<RankingList>();
        }

        #endregion

        #region calendar

        public async Task<CalendarList> GetActiveCalendar(string country = null)
        {
            var request = BaseRequest
                .AppendPathSegment("calendar")
                .AppendPathSegment("active");

            if (!string.IsNullOrEmpty(country))
                request = request.SetQueryParam("country", country);

            return await request.GetJsonAsync<CalendarList>();
        }

        public async Task<CalendarList> GetCalendarHistory(string country = null)
        {
            var request = BaseRequest
                .AppendPathSegment("calendar")
                .AppendPathSegment("history");

            if (!string.IsNullOrEmpty(country))
                request = request.SetQueryParam("country", country);

            return await request.GetJsonAsync<CalendarList>();
        }

        public async Task<CalenderItem> GetCalendarById(int calendarId)
        {
            return await BaseRequest
               .AppendPathSegment("calendar")
               .AppendPathSegment(calendarId)
               .GetJsonAsync<CalenderItem>();
        }

        public async Task<CalendarSearch> GetCalendarSearch(string address, int distance, DistanceUnit units)
        {
            var request = BaseRequest
               .AppendPathSegment("calendar")
               .AppendPathSegment("search")
               .SetQueryParam("address", address);

            if (units == DistanceUnit.Kilometers)
                request = request.SetQueryParam("k", distance);
            else
                request = request.SetQueryParam("m", distance);

            return await request.GetJsonAsync<CalendarSearch>();
        }

        #endregion

        #region pvp

        public async Task<PlayerComparison> GetPvp(int playerOneId, int playerTwoId)
        {
            return await BaseRequest
               .AppendPathSegment("pvp")
               .SetQueryParams(new
               {
                   p1 = playerOneId,
                   p2 = playerTwoId
               })
               .GetJsonAsync<PlayerComparison>();
        }

        #endregion

        #region stats

        public async Task<List<PointsThisYearStat>> GetPointsThisYearStats()
        {
            var json = await BaseRequest
               .AppendPathSegment("stats")
               .AppendPathSegment("points_this_year")
               .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PointsThisYearStat>>();
        }

        public async Task<List<MostEventsStat>> GetMostEventsStats()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("most_events")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<MostEventsStat>>();
        }

        public async Task<List<PlayersByCountryStat>> GetPlayersByCountryStat()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("country_players")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PlayersByCountryStat>>();
        }

        public async Task<List<EventsByYearStat>> GetEventsPerYearStat()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("events_by_year")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<EventsByYearStat>>();
        }

        public async Task<List<PlayersByYearStat>> GetPlayersPerYearStat()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("players_by_year")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<PlayersByYearStat>>();
        }

        public async Task<List<BiggestMoversStat>> GetBiggestMoversStat()
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("biggest_movers")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("stats", false).ToObject<List<BiggestMoversStat>>();
        }

        #endregion

    }
}
