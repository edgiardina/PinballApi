using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRankingViewFilter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("setting")]
        public string Setting { get; set; }
    }
}