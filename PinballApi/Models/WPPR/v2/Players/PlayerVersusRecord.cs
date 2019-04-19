using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerVersusRecord
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("win_count")]
        public int WinCount { get; set; }

        [JsonProperty("loss_count")]
        public int LossCount { get; set; }

        [JsonProperty("tie_count")]
        public int TieCount { get; set; }
    }
}
