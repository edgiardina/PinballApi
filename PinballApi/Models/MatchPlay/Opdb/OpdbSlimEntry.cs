using PinballApi.Converters;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// A cut down OPDB entry. It carries only the fields a machine picker needs, so the slim
    /// export is much smaller than the full one.
    /// </summary>
    public class OpdbSlimEntry
    {
        [JsonPropertyName("opdbId")]
        public string OpdbId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("commonName")]
        public string CommonName { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("physicalMachine")]
        public bool PhysicalMachine { get; set; }

        [JsonPropertyName("manufacturerId")]
        public int? ManufacturerId { get; set; }

        [JsonPropertyName("manufacturerName")]
        public string ManufacturerName { get; set; }

        [JsonPropertyName("entryType")]
        [JsonConverter(typeof(TolerantEnumConverter<OpdbEntryType>))]
        public OpdbEntryType EntryType { get; set; }

        [JsonPropertyName("primaryBackglassImage")]
        public OpdbImage PrimaryBackglassImage { get; set; }
    }
}
