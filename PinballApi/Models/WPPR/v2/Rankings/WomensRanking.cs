using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class WomensRanking
    {
        [JsonProperty("ranking_type")]
        public RankingType RankingType { get; set; }

        [JsonProperty("tournament_type")]
        public TournamentType TournamentType { get; set; }

        [JsonProperty("start_position")]
        public int StartPosition { get; set; }

        [JsonProperty("return_count")]
        public int ReturnCount { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        
        [JsonProperty("rankings")]
        public List<WomensRankingResult> Rankings { get; set; }
    }
}
