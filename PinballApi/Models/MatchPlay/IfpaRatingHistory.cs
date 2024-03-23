using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class IfpaRatingHistory
    {
        [JsonPropertyName("ratingRevisionId")]
        public int RatingRevisionId { get; set; }

        [JsonPropertyName("ratingId")]
        public int RatingId { get; set; }

        [JsonPropertyName("ratingPeriod")]
        public string RatingPeriod { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("rd")]
        public int Rd { get; set; }
    }
}
