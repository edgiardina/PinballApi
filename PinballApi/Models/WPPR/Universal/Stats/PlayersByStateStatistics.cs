using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Stats
{
    public class PlayersByStateStatistics
    {
        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
