using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    public class OpdbImageDimensions
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}
