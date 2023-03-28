using Flurl;
using Flurl.Http;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using PinballApi.Models.MatchPlay;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Arena>> GetArenas(ArenaStatus status = ArenaStatus.Active, List<string> arenaIds = null, int page = 1)
        {
            var request = BaseRequest
                .AppendPathSegment("arenas")
                .SetQueryParam("status", status.ToString().ToLower())
                .SetQueryParam("page", page);

            if (arenaIds != null && arenaIds.Any())
            {
                request = request.SetQueryParams("arenas", string.Join(",", arenaIds));
            }

            var json = await request.GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Arena>>();
        }

        public async Task<List<Game>> GetGames(List<int> tournamentIds, int? playerId = null, int? arenaId = null, int? round = null, int? bank = null, GameStatus? gameStatus = null)
        {
            var request = BaseRequest
                .AppendPathSegment("games")
                .SetQueryParam("tournaments", string.Join(",", tournamentIds));

            if (gameStatus.HasValue)
            {
                request = request.SetQueryParam("status", gameStatus.Value.ToString().ToLower());
            }

            if (bank.HasValue)
            {
                request = request.SetQueryParam("bank", bank.Value);
            }

            if (round.HasValue)
            {
                request = request.SetQueryParam("round", round.Value);
            }

            if (arenaId.HasValue)
            {
                request = request.SetQueryParam("arena", arenaId.Value);
            }

            if (playerId.HasValue)
            {
                request = request.SetQueryParam("player", playerId.Value);
            }

            var json = await request.GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Game>>();
        }

        public async Task<List<Location>> GetLocations(LocationStatus? status = null, List<int> locationIds = null, int page = 1)
        {
            var request = BaseRequest
                .AppendPathSegment("locations")
                .SetQueryParam("page", page); ;

            if (locationIds != null && locationIds.Any())
            {
                request = request.SetQueryParams("locations", string.Join(",", locationIds));
            }

            if (status.HasValue)
            {
                request = request.SetQueryParam("status", status.Value.ToString().ToLower());
            }

            var json = await request.GetStringAsync();

            return JObject.Parse(json)
                .SelectToken("data", false).ToObject<List<Location>>();
        }
    }
}
