using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v1.Statistics
{
    public class PlayersByCountryStat
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
