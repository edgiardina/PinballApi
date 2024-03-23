using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class PlayerStanding
    {
        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("stateprov")]
        public string Stateprov { get; set; }

        [JsonPropertyName("wppr_points")]
        public string WpprPoints { get; set; }

        [JsonPropertyName("event_count")]
        public int EventCount { get; set; }

        [JsonPropertyName("win_count")]
        public int WinCount { get; set; }
    }
}
