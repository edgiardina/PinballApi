using PinballApi.Converters;
using PinballApi.Models.WPPR.Universal.Series;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class Player : PlayerBase
    {
        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("excluded_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ExcludedFlag { get; set; }

        [JsonPropertyName("age")]
        [JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
        public int? Age { get; set; }

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
