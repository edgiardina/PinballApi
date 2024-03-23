using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerSearch
    {
        [JsonPropertyName("filter")]
        public PlayerSearchFilter SearchFilter { get; set; }

        [JsonPropertyName("count")]
        public int SearchCount { get; set; }

        [JsonPropertyName("results")]
        public List<Player> Results { get; set; }
    }
}
