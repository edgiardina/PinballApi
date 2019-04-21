using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class WpprRanking
    {
        [JsonProperty("ranking_type")]
        public RankingType RankingType { get; set; }

        [JsonProperty("start_position")]
        public int StartPosition { get; set; }

        [JsonProperty("return_count")]
        public int ReturnCount { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("sort")]
        public Sort Sort { get; set; }

        [JsonProperty("rankings")]
        public List<RankingResult> Rankings { get; set; }
    }
}
