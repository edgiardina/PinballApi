using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class Sizes
    {
        [JsonProperty("medium")]
        public Medium Medium { get; set; }

        [JsonProperty("large")]
        public Large Large { get; set; }

        [JsonProperty("small")]
        public Small Small { get; set; }
    }
}
