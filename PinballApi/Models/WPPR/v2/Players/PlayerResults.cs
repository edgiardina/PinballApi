using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerResults
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("results_type")]
        public ResultType ResultsType { get; set; }

        [JsonProperty("results_count")]
        public int ResultsCount { get; set; }

        [JsonProperty("rank_type")]
        public RankingType RankingType { get; set; }

        [JsonProperty("active_results")]
        public List<PlayerResult> Results { get; set; }
    }
}
