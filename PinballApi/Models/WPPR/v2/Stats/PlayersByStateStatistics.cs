using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class PlayersByStateStatistics
    {
        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
