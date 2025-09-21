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

        [JsonPropertyName("total_results")]
        public string TotalResults { get; set; }

        [JsonPropertyName("results")]
        public List<PlayerSearchResult> Results { get; set; }
    }
}
