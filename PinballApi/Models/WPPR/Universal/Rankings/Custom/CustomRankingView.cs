using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings.Custom
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
