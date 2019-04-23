using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerSearch
    {
        [JsonProperty("search_filter")]
        public PlayerSearchFilter SearchFilter { get; set; }

        [JsonProperty("search_count")]
        public int SearchCount { get; set; }

        [JsonProperty("search")]
        public List<Player> Results { get; set; }
    }
}
