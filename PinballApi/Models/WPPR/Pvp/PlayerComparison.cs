using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Pvp
{
    public class PlayerComparison
    {
        [JsonProperty("p1_player_id")]
        public int P1PlayerId { get; set; }

        [JsonProperty("p2_player_id")]
        public int P2PlayerId { get; set; }

        [JsonProperty("p1_first_name")]
        public string P1FirstName { get; set; }

        [JsonProperty("p1_last_name")]
        public string P1LastName { get; set; }

        [JsonProperty("p2_first_name")]
        public string P2FirstName { get; set; }

        [JsonProperty("p2_last_name")]
        public string P2LastName { get; set; }

        [JsonProperty("p1_country_code")]
        public string P1CountryCode { get; set; }

        [JsonProperty("p2_country_code")]
        public string P2CountryCode { get; set; }

        [JsonProperty("p1_excluded")]
        public string P1Excluded { get; set; }

        [JsonProperty("p2_excluded")]
        public string P2Excluded { get; set; }

        [JsonProperty("pvp")]
        public List<Pvp> Pvp { get; set; }
    }
}
