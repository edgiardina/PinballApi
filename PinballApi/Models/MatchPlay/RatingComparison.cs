using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class RatingComparison
    {
        [JsonPropertyName("globalRatingRange")]
        public GlobalRatingRange GlobalRatingRange { get; set; }

        [JsonPropertyName("ratings")]
        public List<Rating> Ratings { get; } = new List<Rating>();

        //TODO: implement denormalized records
        //[JsonPropertyName("records")]
        //public Records Records { get; set; }
    }
}
