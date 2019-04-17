using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerComparisons
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("total_competitors")]
        public int TotalCompetitors { get; set; }

        [JsonProperty("pvp")]
        public List<PlayerVersusRecord> Pvp { get; set; }
    }
}
