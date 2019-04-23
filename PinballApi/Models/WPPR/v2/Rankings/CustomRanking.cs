using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRanking
    {
        [JsonProperty("view_id")]
        public int ViewId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("start_position")]
        public int StartPosition { get; set; }

        [JsonProperty("return_count")]
        public int ReturnCount { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("view_results")]
        public List<CustomRankingViewResult> ViewResults { get; set; }

        [JsonProperty("tournaments")]
        public List<Tournament> Tournaments { get; set; }

        [JsonProperty("view_filters")]
        public List<CustomRankingViewFilter> ViewFilters { get; set; }
    }
}
