using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public abstract class PlayerBase
    {
        [JsonPropertyName("player_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("stateprov")]
        public string Stateprov { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("profile_photo_url")]
        public string ProfilePhotoUrl { get; set; }
    }
}