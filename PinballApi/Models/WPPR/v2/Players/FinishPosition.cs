using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class FinishPosition
    {
        [JsonProperty("player_1")]
        public int PlayerOne { get; set; }
        [JsonProperty("player_2")]
        public int PlayerTwo { get; set; }
    }
}
