using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsPayout
    {
        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("prize_value")]
        public double PrizeValue { get; set; }
    }
}
