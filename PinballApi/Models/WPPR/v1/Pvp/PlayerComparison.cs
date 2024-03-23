using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Pvp
{
    public class PlayerComparison
    {
        [JsonPropertyName("p1_player_id")]
        public int P1PlayerId { get; set; }

        [JsonPropertyName("p2_player_id")]
        public int P2PlayerId { get; set; }

        [JsonPropertyName("p1_first_name")]
        public string P1FirstName { get; set; }

        [JsonPropertyName("p1_last_name")]
        public string P1LastName { get; set; }

        [JsonPropertyName("p2_first_name")]
        public string P2FirstName { get; set; }

        [JsonPropertyName("p2_last_name")]
        public string P2LastName { get; set; }

        [JsonPropertyName("p1_country_code")]
        public string P1CountryCode { get; set; }

        [JsonPropertyName("p2_country_code")]
        public string P2CountryCode { get; set; }

        [JsonPropertyName("p1_excluded")]
        public string P1Excluded { get; set; }

        [JsonPropertyName("p2_excluded")]
        public string P2Excluded { get; set; }

        [JsonPropertyName("pvp")]
        public List<Pvp> Pvp { get; set; }
    }
}
