using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings.Custom
{
    public class ViewFilter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("setting")]
        public string Setting { get; set; }
    }
}