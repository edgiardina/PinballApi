using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRankingViewFilter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("setting")]
        public string Setting { get; set; }
    }
}