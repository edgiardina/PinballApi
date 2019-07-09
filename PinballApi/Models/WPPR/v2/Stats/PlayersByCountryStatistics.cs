using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class PlayersByCountryStatistics
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
