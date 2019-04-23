using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerHistory
    {
        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("rank_history")]
        public List<RankHistory> RankHistory { get; set; }

        [JsonProperty("rating_history")]
        public List<RatingHistory> RatingHistory { get; set; }
    }
}
