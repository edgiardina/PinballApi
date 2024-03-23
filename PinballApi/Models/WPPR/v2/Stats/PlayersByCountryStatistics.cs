using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class PlayersByCountryStatistics
    {
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
