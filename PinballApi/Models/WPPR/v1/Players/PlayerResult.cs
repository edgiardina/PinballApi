using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerResult
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("results_count")]
        public int ResultsCount { get; set; }

        [JsonPropertyName("results")]
        public IList<Result> Results { get; set; }
    }
}
