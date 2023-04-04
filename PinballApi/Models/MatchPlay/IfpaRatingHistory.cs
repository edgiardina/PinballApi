using Newtonsoft.Json;

namespace PinballApi.Models.MatchPlay
{
    public class IfpaRatingHistory
    {
        [JsonProperty("ratingRevisionId")]
        public int RatingRevisionId { get; set; }

        [JsonProperty("ratingId")]
        public int RatingId { get; set; }

        [JsonProperty("ratingPeriod")]
        public string RatingPeriod { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("rd")]
        public int Rd { get; set; }
    }
}
