using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v1.Statistics
{
    public class EventsByYearStat
    {
        [JsonPropertyName("Year")]
        public int Year { get; set; }

        [JsonPropertyName("country_count")]
        public int CountryCount { get; set; }

        [JsonPropertyName("periodic_count")]
        public int PeriodicCount { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
