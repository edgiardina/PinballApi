using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.Players
{
    public class RatingHistory
    {
        [JsonProperty("rating_date")]
        public DateTime RatingDate { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }
    }
}
