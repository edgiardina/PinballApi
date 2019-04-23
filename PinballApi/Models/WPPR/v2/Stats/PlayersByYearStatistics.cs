using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class PlayersByYearStatistics
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("previous_year_count")]
        public int PreviousYearCount { get; set; }

        [JsonProperty("previous_2_year_count")]
        public int Previous2_YearCount { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
