using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CountryList
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("country")]
        public List<Country> Country { get; set; }
    }
}
