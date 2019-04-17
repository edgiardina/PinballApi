using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.v2.Players;
using System.Linq;

namespace PinballApi
{
    public class PinballRankingApiV2 : BasePinballRankingApi
    {
        protected override PinballRankingApiVersion ApiVersion => PinballRankingApiVersion.v2beta;

        public PinballRankingApiV2(string apiKey) : base(apiKey)
        {
            
        }

        #region player

        public async Task<Player> GetPlayerRecord(int playerId)
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

        #endregion
    }
}
