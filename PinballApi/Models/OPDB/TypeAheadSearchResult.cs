using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class TypeAheadSearchResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("supplementary")]
        public string Supplementary { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }
}
