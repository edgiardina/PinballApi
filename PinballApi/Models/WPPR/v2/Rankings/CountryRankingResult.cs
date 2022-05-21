using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CountryRankingResult : RankingResult
    {
        [JsonProperty("current_wppr_rank")]
        public override int CurrentWpprRank { get; set; }
    }
}
