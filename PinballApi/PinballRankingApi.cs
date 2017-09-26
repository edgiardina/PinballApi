using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.Calendar;
using PinballApi.Models.WPPR.Players;
using PinballApi.Models.WPPR.Pvp;
using PinballApi.Models.WPPR.Rankings;
using PinballApi.Models.WPPR.Statistics;
using PinballApi.Models.WPPR.Tournaments;
using Flurl.Http;
using Flurl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi
{
    public class PinballRankingApi
    {
        private readonly string ApiKey;
        private const string ifpaBaseUrl = "https://api.ifpapinball.com/";
        private const string apiVersion = "v1";
        private Url BaseRequest => "https://api.ifpapinball.com/v1/".SetQueryParams(new { api_key = ApiKey });
        
        public PinballRankingApi(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentOutOfRangeException("apiKey", "apiKey cannot be null or empty.");

            ApiKey = apiKey;
        }

        #region player

        public async Task<PlayerRecord> GetPlayerRecord(int playerId)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .GetJsonAsync<PlayerRecord>();
        }
        /*
        public async Task<PlayerResult> GetPlayerResults(int playerId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}/results";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddUrlSegment("id", playerId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<PlayerResult>(restRequest);
            return response2.Data;
        }

        public async Task<PlayerComparisons> GetPlayerComparisons(int playerId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}/pvp";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddUrlSegment("id", playerId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<PlayerComparisons>(restRequest);
            return response2.Data;
        }

        public async Task<PlayerSearch> SearchForPlayerByName(string name)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/search";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddQueryParameter("q", name);

            var response2 = await restClient.ExecuteTaskAsync<PlayerSearch>(restRequest);
            return response2.Data;
        }

        public async Task<PlayerSearch> SearchForPlayerByEmail(string email)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/search";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddQueryParameter("email", email);

            var response2 = await restClient.ExecuteTaskAsync<PlayerSearch>(restRequest);
            return response2.Data;
        }

        public async Task<PlayerSearch> GetCountryDirectors()
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/country_directors";
            restRequest.AddUrlSegment("route", "player");

            var response2 = await restClient.ExecuteTaskAsync<PlayerSearch>(restRequest);
            return response2.Data;
        }

        public async Task<PlayerHistory> GetPlayerHistory(int playerId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}/history";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddUrlSegment("id", playerId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<PlayerHistory>(restRequest);
            return response2.Data;
        }

        #endregion

        #region tournament

        public async Task<Tournament> GetTournament(int tournamentId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}";
            restRequest.AddUrlSegment("route", "tournament");
            restRequest.AddUrlSegment("id", tournamentId.ToString());
            restRequest.RootElement = "tournament";

            var response2 = await restClient.ExecuteTaskAsync<Tournament>(restRequest);
            return response2.Data;
        }

        public async Task<TournamentResult> GetTournamentResults(int tournamentId, int? eventId = null, DateTime? tournamentDate = null)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}/results";
            restRequest.AddUrlSegment("route", "tournament");
            restRequest.AddUrlSegment("id", tournamentId.ToString());

            if(eventId.HasValue)
                restRequest.AddQueryParameter("event_id", eventId.Value.ToString());

            if(tournamentDate.HasValue)
                restRequest.AddQueryParameter("tour_date", tournamentDate.Value.ToString("yyyy-MM-dd"));

            restRequest.RootElement = "tournament";

            var response2 = await restClient.ExecuteTaskAsync<TournamentResult>(restRequest);
            return response2.Data;
        }

        public async Task<TournamentList> GetTournamentList(int startPosition = 1, int count = 50)
        {
            if (count > 250 || count < 1)
                throw new ArgumentException("Count must be less than or equal to 250", "count");

            if(startPosition < 1)
                throw new ArgumentException("Start Positon must be a positive integer", "startPosition");

            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/list";
            restRequest.AddUrlSegment("route", "tournament");

            if (startPosition > 1)
                restRequest.AddQueryParameter("start_pos", startPosition.ToString());

            if (count != 50)
                restRequest.AddQueryParameter("count", count.ToString());
            
            var response2 = await restClient.ExecuteTaskAsync<TournamentList>(restRequest);
            return response2.Data;
        }

        public async Task<TournamentSearch> TournamentSearch(string tournamentName)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/search";
            restRequest.AddUrlSegment("route", "tournament");
            restRequest.AddQueryParameter("q", tournamentName);

            var response2 = await restClient.ExecuteTaskAsync<TournamentSearch>(restRequest);
            return response2.Data;
        }

        #endregion

        #region ranking

        public async Task<RankingList> GetRankings(int startPosition = 1, int count = 50, RankingOrder order = RankingOrder.points)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.AddUrlSegment("route", "rankings");
            restRequest.AddQueryParameter("start_pos", startPosition.ToString());
            restRequest.AddQueryParameter("count", count.ToString());
            restRequest.AddQueryParameter("order", Enum.GetName(typeof(RankingOrder), order));

            var response2 = await restClient.ExecuteTaskAsync<RankingList>(restRequest);
            return response2.Data;
        }

        #endregion

        #region calendar

        public async Task<CalendarList> GetActiveCalendar(string country = null)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/active";
            restRequest.AddUrlSegment("route", "calendar");

            if (!string.IsNullOrEmpty(country))
                restRequest.AddQueryParameter("country", country);

            var response2 = await restClient.ExecuteTaskAsync<CalendarList>(restRequest);
            return response2.Data;
        }

        public async Task<CalendarList> GetCalendarHistory(string country = null)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/history";
            restRequest.AddUrlSegment("route", "calendar");

            if (!string.IsNullOrEmpty(country))
                restRequest.AddQueryParameter("country", country);

            var response2 = await restClient.ExecuteTaskAsync<CalendarList>(restRequest);
            return response2.Data;
        }

        public async Task<CalenderItem> GetCalendarById(int calendarId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}";
            restRequest.AddUrlSegment("route", "calendar");
            restRequest.AddUrlSegment("id", calendarId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<CalenderItem>(restRequest);
            return response2.Data;
        }


        public async Task<CalendarSearch> GetCalendarSearch(string address, int distance, DistanceUnit units)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/search";
            restRequest.AddUrlSegment("route", "calendar");
            restRequest.AddQueryParameter("address", address);

            if (units == DistanceUnit.Kilometers)
                restRequest.AddQueryParameter("k", distance.ToString());
            else
                restRequest.AddQueryParameter("m", distance.ToString());

            var response2 = await restClient.ExecuteTaskAsync<CalendarSearch>(restRequest);
            return response2.Data;
        }


        #endregion

        #region pvp

        public async Task<PlayerComparison> GetPvp(int playerOneId, int playerTwoId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.AddUrlSegment("route", "pvp");
            restRequest.AddQueryParameter("p1", playerOneId.ToString());
            restRequest.AddQueryParameter("p2", playerTwoId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<PlayerComparison>(restRequest);
            return response2.Data;
        }

        #endregion

        #region stats

        public async Task<List<PointsThisYearStat>> GetPointsThisYearStats()
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/points_this_year";
            restRequest.AddUrlSegment("route", "stats");
            restRequest.RootElement = "stats";

            var response2 = await restClient.ExecuteTaskAsync<List<PointsThisYearStat>>(restRequest);
            return response2.Data;
        }

        public async Task<List<MostEventsStat>> GetMostEventsStats()
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/most_events";
            restRequest.AddUrlSegment("route", "stats");
            restRequest.RootElement = "stats";

            var response2 = await restClient.ExecuteTaskAsync<List<MostEventsStat>>(restRequest);
            return response2.Data;
        }

        public async Task<List<PlayersByCountryStat>> GetPlayersByCountryStat()
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/country_players";
            restRequest.AddUrlSegment("route", "stats");
            restRequest.RootElement = "stats";

            var response2 = await restClient.ExecuteTaskAsync<List<PlayersByCountryStat>>(restRequest);
            return response2.Data;
        }

        public async Task<List<EventsByYearStat>> GetEventsPerYearStat()
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/events_by_year";
            restRequest.AddUrlSegment("route", "stats");
            restRequest.RootElement = "stats";

            var response2 = await restClient.ExecuteTaskAsync<List<EventsByYearStat>>(restRequest);
            return response2.Data;
        }

        public async Task<List<PlayersByYearStat>> GetPlayersPerYearStat()
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/players_by_year";
            restRequest.AddUrlSegment("route", "stats");
            restRequest.RootElement = "stats";

            var response2 = await restClient.ExecuteTaskAsync<List<PlayersByYearStat>>(restRequest);
            return response2.Data;
        }
        
        public async Task<List<BiggestMoversStat>> GetBiggestMoversStat()
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/biggest_movers";
            restRequest.AddUrlSegment("route", "stats");
            restRequest.RootElement = "stats";

            var response2 = await restClient.ExecuteTaskAsync<List<BiggestMoversStat>>(restRequest);
            return response2.Data;
        }
          */
        #endregion

    }
}
