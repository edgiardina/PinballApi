using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerSearchFilter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("tournament")]
        public string Tournament { get; set; }

        [JsonPropertyName("tournament_position")]
        public string Tourpos { get; set; }
    }
}