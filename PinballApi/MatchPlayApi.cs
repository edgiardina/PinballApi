using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using PinballApi.Models.MatchPlay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi
{
    public class MatchPlayApi
    {
        protected readonly string ApiToken;
        protected IFlurlRequest BaseRequest => $"https://next.matchplay.events/api/"
                                                              .WithOAuthBearerToken(ApiToken)
                                                              .WithHeader("Content-Type", "application/json")
                                                              .WithHeader("Accept", "application/json");

        public MatchPlayApi(string apiToken)
        {
            this.ApiToken = apiToken;
        }

        public async Task<Dashboard> GetDashboard()
        {
            return await BaseRequest
                .AppendPathSegment("dashboard")
                .GetJsonAsync<Dashboard>();
        }

        public async Task<List<Arena>> GetArenas()
        {
            var json = await BaseRequest
                .AppendPathSegment("arenas")
                .GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Arena>>();
        }
    }
}
