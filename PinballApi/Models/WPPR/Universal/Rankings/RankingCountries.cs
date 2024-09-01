using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class RankingCountries
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("country")]
        public List<Country> Country { get; set; }
    }
}
