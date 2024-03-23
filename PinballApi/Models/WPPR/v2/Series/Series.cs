using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class Series
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("years")]
        public List<int> Years { get; set; }
    }
}
