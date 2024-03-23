using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerSearch
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("search")]
        public List<Search> Search { get; set; }
    }
}
