using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStateProvinceStandings
    {
        [JsonProperty("player_standings")]
        public List<PlayerStanding> PlayerStandings { get; set; }

        [JsonProperty("stats")]
        public List<NacsStateProvinceStatistics> Statistics { get; set; }

        [JsonProperty("payouts")]
        public List<NacsPayout> Payouts { get; set; }
    }
}
