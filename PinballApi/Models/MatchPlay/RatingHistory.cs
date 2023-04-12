using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class RatingHistory
    {
        [JsonPropertyName("ratingPeriod")]
        public string RatingPeriod { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("upperBound")]
        public int UpperBound { get; set; }

        [JsonPropertyName("lowerBound")]
        public int LowerBound { get; set; }
    }
}
