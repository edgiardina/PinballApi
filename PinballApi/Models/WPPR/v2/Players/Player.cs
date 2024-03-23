using System.Text.Json.Serialization;
using PinballApi.Models.v2.WPPR;
using PinballApi.Models.WPPR.v1.Players;
using System;
using System.Collections.Generic;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v2.Players
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

        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("age")]
        [JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
        public int? Age { get; set; }

        [JsonPropertyName("womens_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool WomensFlag { get; set; }

        [JsonPropertyName("excluded_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ExcludedFlag { get; set; }

        [JsonPropertyName("ifpa_registered")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool IfpaRegistered { get; set; }

        [JsonPropertyName("profile_photo")]
        public Uri ProfilePhoto { get; set; }

        [JsonPropertyName("player_stats")]
        public PlayerStats PlayerStats { get; set; }

        [JsonPropertyName("series")]
        public List<ChampionshipSeries> ChampionshipSeries { get; set; }
    }
}
