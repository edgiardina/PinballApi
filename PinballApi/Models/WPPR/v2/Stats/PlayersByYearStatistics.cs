using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class PlayersByYearStatistics
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("previous_year_count")]
        public int PreviousYearCount { get; set; }

        [JsonPropertyName("previous_2_year_count")]
        public int Previous2_YearCount { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
