using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class Sizes
    {
        [JsonPropertyName("medium")]
        public Medium Medium { get; set; }

        [JsonPropertyName("large")]
        public Large Large { get; set; }

        [JsonPropertyName("small")]
        public Small Small { get; set; }
    }
}
