using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class MachineImage
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("urls")]
        public MachineImageUrl Urls { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }
    }
}
