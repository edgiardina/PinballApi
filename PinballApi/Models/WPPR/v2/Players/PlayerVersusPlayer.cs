using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerVersusPlayer
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("total_competitors")]
        public int TotalCompetitors { get; set; }

        [JsonProperty("pvp")]
        public List<PlayerVersusRecord> PlayerVersusPlayerRecords { get; set; }

    }
}
