using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerSearch
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("search")]
        public List<Search> Search { get; set; }
    }
}
