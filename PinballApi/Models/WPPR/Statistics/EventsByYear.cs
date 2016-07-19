using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Statistics
{
    public class EventsByYear
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stats")]
        public IList<EventsByYearStat> Stats { get; set; }
    }
}
