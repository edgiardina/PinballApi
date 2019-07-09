using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class PlayerStanding
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

        [JsonProperty("stateprov")]
        public string Stateprov { get; set; }

        [JsonProperty("wppr_points")]
        public string WpprPoints { get; set; }

        [JsonProperty("event_count")]
        public int EventCount { get; set; }

        [JsonProperty("win_count")]
        public int WinCount { get; set; }
    }
}
