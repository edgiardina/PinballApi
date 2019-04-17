using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v1.Statistics
{
    public class EventsByYearStat
    {
        [JsonProperty("Year")]
        public int Year { get; set; }

        [JsonProperty("country_count")]
        public int CountryCount { get; set; }

        [JsonProperty("periodic_count")]
        public int PeriodicCount { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
