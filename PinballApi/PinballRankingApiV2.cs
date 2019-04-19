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

        public async Task<NacsStatistics> GetNacsStatistics()
        {
            return await BaseRequest
                    .AppendPathSegment("nacs/stats")
                    .GetJsonAsync<NacsStatistics>();
        }

        #endregion
    }
}
