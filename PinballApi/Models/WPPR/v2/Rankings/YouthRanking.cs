using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class YouthRanking
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
        public List<RankingResult> Rankings { get; set; }
    }
}
