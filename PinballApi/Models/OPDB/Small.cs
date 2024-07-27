using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class Small
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}
