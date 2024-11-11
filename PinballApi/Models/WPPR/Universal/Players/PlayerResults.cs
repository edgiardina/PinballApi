using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PinballApi.Models.WPPR.v2.Tournaments;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerResults
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("results_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultType ResultsType { get; set; }

        [JsonPropertyName("results_count")]
        public int ResultsCount { get; set; }

        [JsonPropertyName("rank_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RankingType RankingType { get; set; }

        [JsonPropertyName("results")]
        public List<PlayerResult> Results { get; set; }
    }
}
