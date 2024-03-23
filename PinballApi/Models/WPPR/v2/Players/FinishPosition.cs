using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class FinishPosition
    {
        [JsonPropertyName("player_1")]
        public int PlayerOne { get; set; }
        [JsonPropertyName("player_2")]
        public int PlayerTwo { get; set; }
    }
}
