using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class Series
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
