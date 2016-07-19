﻿using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.Players;
using RestSharp;
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
        }

        public async Task<PlayerRecord> GetPlayerRecord(int playerId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddUrlSegment("id", playerId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<PlayerRecord>(restRequest);
            return response2.Data;
        }

        public async Task<TournamentResults> GetPlayerResult(int playerId)
        {
            var restRequest = GenerateDefaultRestRequest();
            restRequest.Resource += "/{id}/results";
            restRequest.AddUrlSegment("route", "player");
            restRequest.AddUrlSegment("id", playerId.ToString());

            var response2 = await restClient.ExecuteTaskAsync<TournamentResults>(restRequest);
            return response2.Data;
        }



        private RestRequest GenerateDefaultRestRequest()
        {
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.Resource = "v1/{route}";
            restRequest.AddUrlSegment("version", apiVersion);
            restRequest.AddQueryParameter("api_key", ApiKey);
            return restRequest;
        }
    }
}
