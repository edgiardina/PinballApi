using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Rankings
{
    public class RankingList
    {
        [JsonPropertyName("total_count")]
        public string TotalCount { get; set; }

        [JsonPropertyName("sub_category")]
        public string SubCategory { get; set; }

        [JsonPropertyName("rank_country_name")]
        public string RankCountryName { get; set; }

        [JsonPropertyName("rankings")]
        public List<Ranking> Rankings { get; set; }
    }
}
