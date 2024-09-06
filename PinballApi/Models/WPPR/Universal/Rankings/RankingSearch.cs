using PinballApi.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class RankingSearch
    {
        [JsonPropertyName("ranking_type")]
        public RankingType RankingType { get; set; }

        [JsonPropertyName("tournament_type")]
        public RankingSystem RankingSystem { get; set; }

        [JsonPropertyName("start_position")]
        public int StartPosition { get; set; }

        [JsonPropertyName("return_count")]
        public int ReturnCount { get; set; }

        [JsonPropertyName("total_count")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TotalCount { get; set; }

        [JsonPropertyName("rankings")]
        public List<Ranking> Rankings { get; set; }
    }
}
