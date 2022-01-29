using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class MachineImageUrl
    {
        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }
    }
}
