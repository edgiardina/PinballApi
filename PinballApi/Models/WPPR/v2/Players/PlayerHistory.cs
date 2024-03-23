using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerHistory
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("rank_history")]
        public List<RankHistory> RankHistory { get; set; }

        [JsonPropertyName("rating_history")]
        public List<RatingHistory> RatingHistory { get; set; }
    }
}
