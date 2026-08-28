using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    public class OpdbImageSizes
    {
        [JsonPropertyName("small")]
        public OpdbImageDimensions Small { get; set; }

        [JsonPropertyName("medium")]
        public OpdbImageDimensions Medium { get; set; }

        [JsonPropertyName("large")]
        public OpdbImageDimensions Large { get; set; }
    }
}
