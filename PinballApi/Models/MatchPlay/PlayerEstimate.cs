using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class PlayerEstimate
    {
        [JsonPropertyName("ifpaId")]
        public int IfpaId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("totalEvents")]
        public int TotalEvents { get; set; }

        [JsonPropertyName("baseValue")]
        public double BaseValue { get; set; }

        [JsonPropertyName("ratingValue")]
        public double RatingValue { get; set; }

        [JsonPropertyName("ratingValueIncluded")]
        public bool RatingValueIncluded { get; set; }

        [JsonPropertyName("rankValue")]
        public double RankValue { get; set; }

        [JsonPropertyName("rankValueIncluded")]
        public bool RankValueIncluded { get; set; }

        [JsonPropertyName("wpprValue")]
        public double WpprValue { get; set; }
    }
}
