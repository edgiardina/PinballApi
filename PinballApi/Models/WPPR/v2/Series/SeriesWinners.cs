using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesWinners
    {
        [JsonProperty("series_code")]
        public string SeriesCode { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("results")]
        public List<SeriesResult> Results { get; set; }
    }
}
