using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class Rating
    {
        [JsonPropertyName("ratingId")]
        public int RatingId { get; set; }

        [JsonPropertyName("userId")]
        public int? UserId { get; set; }

        [JsonPropertyName("ifpaId")]
        public int? IfpaId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("rating")]
        public int RatingNumber { get; set; }

        [JsonPropertyName("rd")]
        public int Rd { get; set; }

        [JsonPropertyName("calculatedRd")]
        public int CalculatedRd { get; set; }

        [JsonPropertyName("lowerBound")]
        public int LowerBound { get; set; }

        [JsonPropertyName("lastRatingPeriod")]
        public DateTime LastRatingPeriod { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }
    }
}
