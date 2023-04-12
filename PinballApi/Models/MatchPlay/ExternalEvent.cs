using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class ExternalEvent
    {
        [JsonPropertyName("extEventId")]
        public int ExtEventId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("submitterName")]
        public string SubmitterName { get; set; }

        [JsonPropertyName("submitterId")]
        public int SubmitterId { get; set; }
    }
}
