using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings.Custom
{
    public class CustomRankingViewResult
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
        public List<ViewResult> ViewResults { get; set; }

        [JsonPropertyName("tournaments")]
        public List<CustomViewTournament> Tournaments { get; set; }

        [JsonPropertyName("view_filters")]
        public List<ViewFilter> ViewFilters { get; set; }
    }
}