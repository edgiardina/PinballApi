using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.OPDB
{
    public class TypeAheadSearchResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("supplementary")]
        public string Supplementary { get; set; }

        [JsonPropertyName("display")]
        public string Display { get; set; }
    }
}
