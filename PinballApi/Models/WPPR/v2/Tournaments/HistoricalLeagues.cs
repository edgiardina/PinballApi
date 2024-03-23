using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class HistoricalLeagues
    {
        [JsonPropertyName("total_entries")]
        public int TotalEntries { get; set; }

        [JsonPropertyName("league_state")]
        public LeagueState LeagueState { get; set; }

        [JsonPropertyName("leagues")]
        public IList<HistoricalLeague> Leagues { get; set; }
    }
}
