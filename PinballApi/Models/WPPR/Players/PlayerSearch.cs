using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Players
{
    public class PlayerSearch
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("search")]
        public IList<Search> Search { get; set; }
    }
}
