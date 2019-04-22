using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class ActiveLeagues
    {
        [JsonProperty("total_entries")]
        public int TotalEntries { get; set; }

        [JsonProperty("league_state")]
        public LeagueState LeagueState { get; set; }

        [JsonProperty("leagues")]
        public IList<ActiveLeague> Leagues { get; set; }
    }
}
