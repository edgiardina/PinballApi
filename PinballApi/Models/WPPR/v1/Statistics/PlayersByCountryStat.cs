using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v1.Statistics
{
    public class PlayersByCountryStat
    {
        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
