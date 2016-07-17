using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.Ranking.Statistics
{
    public class BiggestMovers
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stats")]
        public IList<BiggestMoversStat> Stats { get; set; }
    }
}
