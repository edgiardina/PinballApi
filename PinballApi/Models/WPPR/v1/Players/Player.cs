using System.Text.Json.Serialization;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class Player
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("age")]
        public string Age { get; set; }

        [JsonPropertyName("excluded_flag")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool ExcludedFlag { get; set; }

        [JsonPropertyName("ifpa_registered")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IfpaRegistered { get; set; }
    }
}
