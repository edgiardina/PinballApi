using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace PinballApi.Models.OPDB
{
    public class PinballMachine
    {
        [JsonPropertyName("opdb_id")]
        public string OpdbId { get; set; }

        [JsonPropertyName("is_machine")]
        public bool IsMachine { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("common_name")]
        public string CommonName { get; set; }

        [JsonPropertyName("shortname")]
        public string Shortname { get; set; }

        [JsonPropertyName("physical_machine")]
        public int PhysicalMachine { get; set; }

        [JsonPropertyName("ipdb_id")]
        public int? IpdbId { get; set; }

        [JsonPropertyName("manufacture_date")]
        public DateTime ManufactureDate { get; set; }

        [JsonPropertyName("manufacturer")]
        public Manufacturer Manufacturer { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("display")]
        public string Display { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("features")]
        public List<string> Features { get; set; }

        [JsonPropertyName("keywords")]
        public List<string> Keywords { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("images")]
        public List<MachineImage> Images { get; set; }

        [JsonPropertyName("is_alias")]
        public bool? IsAlias { get; set; }
    }
}
