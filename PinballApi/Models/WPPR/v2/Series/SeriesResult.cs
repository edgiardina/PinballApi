using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesResult
    {
        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("winners")]
        public List<SeriesWinner> Winners { get; set; }

    }
}
