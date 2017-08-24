using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.Tournaments
{
    public class Result
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        
        [JsonProperty("wppr_rank")]
        public int? WpprRank { get; set; }

        [JsonProperty("ratings_value")]
        public string RatingsValue { get; set; }

        [JsonProperty("points")]
        public double Points { get; set; }
    }
}
