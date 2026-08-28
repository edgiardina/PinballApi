using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    public class OpdbImageUrls
    {
        [JsonPropertyName("small")]
        public string Small { get; set; }

        [JsonPropertyName("medium")]
        public string Medium { get; set; }

        [JsonPropertyName("large")]
        public string Large { get; set; }
    }
}
