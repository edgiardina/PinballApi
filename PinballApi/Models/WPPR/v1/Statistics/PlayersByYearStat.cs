using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v1.Statistics
{
    public class PlayersByYearStat
    {
        [JsonProperty("Year")]
        public int Year { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("previous_year_count")]
        public int PreviousYearCount { get; set; }
        
        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
