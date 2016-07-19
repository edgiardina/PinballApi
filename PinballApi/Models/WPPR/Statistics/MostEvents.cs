using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Statistics
{
    public class MostEvents
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stats")]
        public IList<MostEventsStat> Stats { get; set; }
    }

}
