using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.SeriesStats
{
    public class Aggregate
    {
        [JsonPropertyName("sum")]
        public int Sum { get; set; }

        [JsonPropertyName("max")]
        public int Max { get; set; }

        [JsonPropertyName("min")]
        public int Min { get; set; }

        [JsonPropertyName("mean")]
        public decimal Mean { get; set; }

        [JsonPropertyName("median")]
        public decimal Median { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
