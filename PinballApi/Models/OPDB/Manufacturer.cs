using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.OPDB
{
    public class Manufacturer
    {
        [JsonPropertyName("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
