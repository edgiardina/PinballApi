using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v1.Statistics
{
    public class PlayersByYearStat
    {
        [JsonPropertyName("Year")]
        public int Year { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("previous_year_count")]
        public int PreviousYearCount { get; set; }
        
        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
