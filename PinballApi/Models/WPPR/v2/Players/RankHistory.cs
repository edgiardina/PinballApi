using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class RankHistory
    {
        [JsonPropertyName("rank_date")]
        public DateTime RankDate { get; set; }

        [JsonPropertyName("rank_position")]
        public int RankPosition { get; set; }

        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonPropertyName("tournaments_played_count")]
        public int TournamentsPlayed { get; set; }
    }
}
