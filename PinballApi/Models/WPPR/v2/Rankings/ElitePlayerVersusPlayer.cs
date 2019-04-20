using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class ElitePlayerVersusPlayer
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("total_wins")]
        public int TotalWins { get; set; }

        [JsonProperty("total_losses")]
        public int TotalLosses { get; set; }

        [JsonProperty("total_draws")]
        public int TotalDraws { get; set; }

        [JsonProperty("pvp")]
        public List<ElitePlayerVersusPlayerRecord> Records { get; set; }
    }
}
