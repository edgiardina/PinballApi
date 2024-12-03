using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class Payout
    {
        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("prize_fund")]
        public double PrizeFund { get; set; }
    }
}