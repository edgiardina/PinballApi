using PinballApi.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// One record from the Open Pinball Database. An entry is a machine group, a machine or an
    /// alias. Read <see cref="EntryType"/> to tell them apart.
    /// </summary>
    public class OpdbEntry
    {
        /// <summary>
        /// The full OPDB id of this entry. Use <see cref="OpdbIdParts"/> to split it into parts.
        /// </summary>
        [JsonPropertyName("opdbId")]
        public string OpdbId { get; set; }

        /// <summary>
        /// The id of the group this entry belongs to.
        /// </summary>
        [JsonPropertyName("opdbGroup")]
        public string OpdbGroup { get; set; }

        /// <summary>
        /// The id of the machine this entry belongs to. Null on a machine group.
        /// </summary>
        [JsonPropertyName("opdbMachine")]
        public string OpdbMachine { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("commonName")]
        public string CommonName { get; set; }

        /// <summary>
        /// The name in sort order, for example <c>Addams Family, The</c>.
        /// </summary>
        [JsonPropertyName("nameSort")]
        public string NameSort { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("manufactureDate")]
        public DateTime? ManufactureDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Known values are <c>em</c> (electro-mechanical), <c>ss</c> (solid state) and
        /// <c>me</c> (mechanical). Null on a machine group.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Known values are <c>reels</c>, <c>lights</c>, <c>alphanumeric</c>, <c>dmd</c>,
        /// <c>lcd</c> and <c>cga</c>. Null on a machine group.
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; }

        [JsonPropertyName("playerCount")]
        public int? PlayerCount { get; set; }

        /// <summary>
        /// True when the entry stands for a real machine you can play.
        /// </summary>
        [JsonPropertyName("physicalMachine")]
        public bool PhysicalMachine { get; set; }

        [JsonPropertyName("manufacturerId")]
        public int? ManufacturerId { get; set; }

        [JsonPropertyName("ipdbId")]
        public int? IpdbId { get; set; }

        [JsonPropertyName("pinballPrimerUrl")]
        public string PinballPrimerUrl { get; set; }

        [JsonPropertyName("pinballRulesUrl")]
        public string PinballRulesUrl { get; set; }

        [JsonPropertyName("pinballCardsUrl")]
        public string PinballCardsUrl { get; set; }

        [JsonPropertyName("bobsGuideUrl")]
        public string BobsGuideUrl { get; set; }

        [JsonPropertyName("competitionSetupUrl")]
        public string CompetitionSetupUrl { get; set; }

        [JsonPropertyName("competitionNotesUrl")]
        public string CompetitionNotesUrl { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("entryType")]
        [JsonConverter(typeof(TolerantEnumConverter<OpdbEntryType>))]
        public OpdbEntryType EntryType { get; set; }

        [JsonPropertyName("manufacturer")]
        public OpdbManufacturer Manufacturer { get; set; }

        /// <summary>
        /// The people credited on this entry. Empty unless the caller asked for people data.
        /// </summary>
        [JsonPropertyName("people")]
        public List<OpdbPerson> People { get; set; }

        /// <summary>
        /// The images for this entry. Empty unless the caller asked for image data.
        /// </summary>
        [JsonPropertyName("images")]
        public List<OpdbImage> Images { get; set; }

        [JsonPropertyName("features")]
        public List<OpdbFeature> Features { get; set; }

        /// <summary>
        /// True when this entry is a machine group.
        /// </summary>
        [JsonIgnore]
        public bool IsMachineGroup => EntryType == OpdbEntryType.MachineGroup;

        /// <summary>
        /// True when this entry is a machine.
        /// </summary>
        [JsonIgnore]
        public bool IsMachine => EntryType == OpdbEntryType.Machine;

        /// <summary>
        /// True when this entry is an alias of a machine.
        /// </summary>
        [JsonIgnore]
        public bool IsAlias => EntryType == OpdbEntryType.Alias;
    }
}
