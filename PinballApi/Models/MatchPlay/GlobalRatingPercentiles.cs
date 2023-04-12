using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class GlobalRatingPercentiles
    {
        [JsonPropertyName("p50")]
        public double P50 { get; set; }

        [JsonPropertyName("p75")]
        public double P75 { get; set; }

        [JsonPropertyName("p95")]
        public double P95 { get; set; }

        [JsonPropertyName("p99")]
        public double P99 { get; set; }
    }
}
