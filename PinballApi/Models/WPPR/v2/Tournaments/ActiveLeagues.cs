using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class ActiveLeagues
    {
        [JsonPropertyName("total_entries")]
        public int TotalEntries { get; set; }

        [JsonPropertyName("league_state")]
        public LeagueState LeagueState { get; set; }

        [JsonPropertyName("leagues")]
        public IList<ActiveLeague> Leagues { get; set; }
    }
}
