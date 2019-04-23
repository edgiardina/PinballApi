using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerRecord
    {
        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("player_stats")]
        public PlayerStats PlayerStats { get; set; }

    }
}
