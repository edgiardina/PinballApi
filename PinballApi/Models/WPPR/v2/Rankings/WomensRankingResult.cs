using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class WomensRankingResult : RankingResult
    {
        [JsonProperty("current_wppr_rank")]
        public int CurrentWpprRank { get; set; }
    }
}
