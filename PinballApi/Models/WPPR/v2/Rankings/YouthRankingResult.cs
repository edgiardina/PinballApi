using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class YouthRankingResult : RankingResult
    {
        [JsonPropertyName("current_wppr_rank")]
        public int CurrentWpprRank { get; set; }
    }
}
