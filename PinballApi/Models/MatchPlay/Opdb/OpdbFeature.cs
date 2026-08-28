using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    public class OpdbFeature
    {
        [JsonPropertyName("featureId")]
        public int FeatureId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
