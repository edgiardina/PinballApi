using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRankingView
    {
        [JsonProperty("view_id")]
        public int ViewId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}