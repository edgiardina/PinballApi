using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.Calendar;
using PinballApi.Models.WPPR.Players;
using PinballApi.Models.WPPR.Rankings;
using PinballApi.Models.WPPR.Tournaments;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi
{
    public class PinballRankingApi
    {
        private readonly string ApiKey;
        private const string ifpaBaseUrl = "https://api.ifpapinball.com/";
        private readonly RestClient restClient = new RestClient(ifpaBaseUrl);
        private const string apiVersion = "v1";

        public PinballRankingApi(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentOutOfRangeException("apiKey", "apiKey cannot be null or empty.");

            ApiKey = apiKey;
            // Override with Newtonsoft JSON Handler
            //pretty bummed this isn't returning application/json or text/json. 

            restClient.AddHandler("text/html", new JsonDeserializer());
        }

        #region player

        public async Task<PlayerRecord> GetPlayerRecord(int playerId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddUrlSegment("id", playerId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<PlayerRecord>(restRequest);
            return response2.Data;
        }

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

        #endregion

        private RestRequest GenerateDefaultRestRequest()
        {
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.Resource = "{version}/{route}";
            restRequest.AddUrlSegment("version", apiVersion);
            restRequest.AddQueryParameter("api_key", ApiKey);
            return restRequest;
        }

    }
}
