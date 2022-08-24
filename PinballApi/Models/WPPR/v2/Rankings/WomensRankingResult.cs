using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class WomensRankingResult : RankingResult
    {
        [JsonProperty("current_wppr_rank")]
        public override int CurrentWpprRank { get; set; }

        //There's a bad naming convention in the IFPA API so we get around it here. 
        //When you call the women's ranking endpoint for women you get current_wppr_rank and for open you get current_rank
        [JsonProperty("current_rank")]
        private int CurrentRank { set { CurrentWpprRank = value; } }
    }
}
