using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Director
{
    public class Formats
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
