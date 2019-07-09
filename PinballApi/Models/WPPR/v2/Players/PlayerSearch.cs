using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerSearch
    {
        [JsonProperty("filter")]
        public PlayerSearchFilter SearchFilter { get; set; }

        [JsonProperty("count")]
        public int SearchCount { get; set; }

        [JsonProperty("results")]
        public List<Player> Results { get; set; }
    }
}
