using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class RatingHistory
    {
        [JsonPropertyName("rating_date")]
        public DateTime RatingDate { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }
    }
}
