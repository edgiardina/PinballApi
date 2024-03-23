using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class IfpaEstimate
    {
        [JsonPropertyName("players")]
        public List<PlayerEstimate> Players { get; } = new List<PlayerEstimate>();

        [JsonPropertyName("unresolvedNames")]
        public List<string> UnresolvedNames { get; } = new List<string>();

        [JsonPropertyName("baseValue")]
        public double BaseValue { get; set; }

        [JsonPropertyName("ratingValue")]
        public double RatingValue { get; set; }

        [JsonPropertyName("rankValue")]
        public double RankValue { get; set; }

        [JsonPropertyName("wpprValue")]
        public double WpprValue { get; set; }

        [JsonPropertyName("standingsOrder")]
        public List<string> StandingsOrder { get; } = new List<string>();
    }
}
