using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStateProvinceStandings
    {
        [JsonPropertyName("standings")]
        public List<PlayerStanding> PlayerStandings { get; set; }

        [JsonPropertyName("stats")]
        public List<NacsStateProvinceStatistics> Statistics { get; set; }

        [JsonPropertyName("payouts")]
        public List<NacsPayout> Payouts { get; set; }
    }
}
