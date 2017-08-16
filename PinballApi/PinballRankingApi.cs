﻿using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.Players;
using RestSharp;
using RestSharp.Deserializers;
using System;
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

        public async Task<TournamentResults> GetPlayerResults(int playerId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}/results";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddUrlSegment("id", playerId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<TournamentResults>(restRequest);
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
