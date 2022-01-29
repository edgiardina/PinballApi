using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.OPDB
{
    public class PinballMachine
    {
        [JsonProperty("opdb_id")]
        public string OpdbId { get; set; }

        [JsonProperty("is_machine")]
        public bool IsMachine { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("common_name")]
        public string CommonName { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("physical_machine")]
        public int PhysicalMachine { get; set; }

        [JsonProperty("ipdb_id")]
        public int IpdbId { get; set; }

        [JsonProperty("manufacture_date")]
        public string ManufactureDate { get; set; }

        [JsonProperty("manufacturer")]
        public Manufacturer Manufacturer { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("player_count")]
        public int PlayerCount { get; set; }

        [JsonProperty("features")]
        public List<string> Features { get; set; }

        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("images")]
        public List<MachineImage> Images { get; set; }

        [JsonProperty("is_alias")]
        public bool? IsAlias { get; set; }
    }
}
