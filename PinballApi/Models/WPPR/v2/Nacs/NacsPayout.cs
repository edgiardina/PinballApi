using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsPayout
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("prize_value")]
        public double PrizeValue { get; set; }
    }
}
