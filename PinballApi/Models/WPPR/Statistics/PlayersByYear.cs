using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Statistics
{
    public class PlayersByYear
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stats")]
        public IList<PlayersByYearStat> Stats { get; set; }
    }
}
