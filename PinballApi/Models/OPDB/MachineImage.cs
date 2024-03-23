using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class MachineImage
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("urls")]
        public MachineImageUrl Urls { get; set; }

        [JsonPropertyName("sizes")]
        public Sizes Sizes { get; set; }
    }
}
