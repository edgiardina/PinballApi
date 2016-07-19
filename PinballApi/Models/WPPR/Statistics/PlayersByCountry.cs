using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Statistics
{
    class PlayersByCountry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stats")]
        public IList<PlayersByCountryStat> Stats { get; set; }
    }
}
