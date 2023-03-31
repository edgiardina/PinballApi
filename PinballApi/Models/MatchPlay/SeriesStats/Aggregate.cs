using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.SeriesStats
{
    public class Aggregate
    {
        [JsonProperty("sum")]
        public int Sum { get; set; }

        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("min")]
        public int Min { get; set; }

        [JsonProperty("mean")]
        public decimal Mean { get; set; }

        [JsonProperty("median")]
        public decimal Median { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
