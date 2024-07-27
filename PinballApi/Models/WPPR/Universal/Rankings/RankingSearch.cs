using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class RankingSearch
    {
        [JsonPropertyName("ranking_type")]
        public RankingType RankingType { get; set; }

        [JsonPropertyName("rankings")]
        public List<Ranking> Rankings { get; set; }
    }
}
