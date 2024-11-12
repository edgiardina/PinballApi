using PinballApi.Converters;
using PinballApi.Models.WPPR.Universal.Series;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class Player
    {
        [JsonPropertyName("player_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("excluded_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ExcludedFlag { get; set; }

        [JsonPropertyName("age")]
        [JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
        public int? Age { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("stateprov")]
        public string Stateprov { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("ifpa_registered")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool IfpaRegistered { get; set; }

        [JsonPropertyName("womens_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool WomensFlag { get; set; }

        [JsonPropertyName("profile_photo")]
        public Uri ProfilePhoto { get; set; }

        [JsonPropertyName("matchplay_events")]
        public MatchplayEvents MatchplayEvents { get; set; }

        [JsonPropertyName("player_stats")]
        public PlayerStats PlayerStats { get; set; }

        [JsonPropertyName("series")]
        public List<SeriesRank> Series { get; set; }
    }
}
