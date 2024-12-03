using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Tournaments
{
    public class TournamentFormat
    {
        [JsonPropertyName("format_id")]
        public int FormatId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}