using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class EliteRanking
    {
        [JsonProperty("ranking_type")]
        public RankingType RankingType { get; set; }

        [JsonProperty("start_position")]
        public int StartPosition { get; set; }

        [JsonProperty("return_count")]
        public int ReturnCount { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("rankings")]
        public List<EliteRankingResult> Rankings { get; set; }
    }
}
