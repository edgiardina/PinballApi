using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRanking
    {
        [JsonPropertyName("view_id")]
        public int ViewId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("start_position")]
        public int StartPosition { get; set; }

        [JsonPropertyName("return_count")]
        public int ReturnCount { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("view_results")]
        public List<CustomRankingViewResult> ViewResults { get; set; }

        [JsonPropertyName("tournaments")]
        public List<Tournament> Tournaments { get; set; }

        [JsonPropertyName("view_filters")]
        public List<CustomRankingViewFilter> ViewFilters { get; set; }
    }
}
