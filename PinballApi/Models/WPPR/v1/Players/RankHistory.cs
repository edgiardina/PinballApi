using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class RankHistory
    {
        [JsonPropertyName("rank_date")]
        public DateTime RankDate { get; set; }

        [JsonPropertyName("rank_position")]
        public int RankPosition { get; set; }

        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }
    }
}
