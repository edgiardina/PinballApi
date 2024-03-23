using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class YouthRanking
    {
        [JsonPropertyName("ranking_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RankingType RankingType { get; set; }

        [JsonPropertyName("tournament_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TournamentType TournamentType { get; set; }

        [JsonPropertyName("start_position")]
        public int StartPosition { get; set; }

        [JsonPropertyName("return_count")]
        public int ReturnCount { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("rankings")]
        public List<YouthRankingResult> Rankings { get; set; }
    }
}
