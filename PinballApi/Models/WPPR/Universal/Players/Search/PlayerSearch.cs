using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players.Search
{
    public class PlayerSearch
    {
        [JsonPropertyName("search_filter")]
        public PlayerSearchFilter SearchFilter { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("results")]
        public List<Player> Results { get; set; }
    }
}
