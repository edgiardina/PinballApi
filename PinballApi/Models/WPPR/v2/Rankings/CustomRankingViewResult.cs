using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRankingViewResult
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("wppr_rank")]
        public int? WpprRank { get; set; }

        [JsonProperty("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonProperty("event_count")]
        public int EventCount { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }
    }
}
