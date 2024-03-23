using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRankingView
    {
        [JsonPropertyName("view_id")]
        public int ViewId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}