using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class PlayersByStateStatistics
    {
        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
