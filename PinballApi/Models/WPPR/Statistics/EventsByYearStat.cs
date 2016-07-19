using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.Statistics
{
    public class EventsByYearStat
    {
        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("country_count")]
        public string CountryCount { get; set; }

        [JsonProperty("periodic_count")]
        public string PeriodicCount { get; set; }

        [JsonProperty("count")]
        public string Count { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
