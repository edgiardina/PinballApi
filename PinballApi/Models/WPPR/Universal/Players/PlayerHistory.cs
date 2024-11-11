using PinballApi.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerHistory
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("system")]
        [JsonConverter(typeof(PlayerRankingSystemConverter))]
        public PlayerRankingSystem System { get; set; }

        [JsonPropertyName("active_flag")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool ActiveFlag { get; set; }

        [JsonPropertyName("rank_history")]
        public List<RankHistory> RankHistory { get; set; }

        [JsonPropertyName("rating_history")]
        public List<RatingHistory> RatingHistory { get; set; }
    }
}
