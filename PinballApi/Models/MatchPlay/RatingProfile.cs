using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class RatingProfile
    {
        [JsonPropertyName("rating")]
        public Rating Rating { get; set; }

        [JsonPropertyName("ratingHistory")]
        public List<RatingHistory> RatingHistory { get; } = new List<RatingHistory>();

        [JsonPropertyName("globalRatingPercentiles")]
        public GlobalRatingPercentiles GlobalRatingPercentiles { get; set; }
    }
}
