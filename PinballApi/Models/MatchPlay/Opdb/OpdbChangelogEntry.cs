using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// One change to an OPDB id. Read the changelog to repair ids that you stored before OPDB
    /// moved or removed them.
    /// </summary>
    public class OpdbChangelogEntry
    {
        [JsonPropertyName("changelogId")]
        public int ChangelogId { get; set; }

        [JsonPropertyName("action")]
        [JsonConverter(typeof(TolerantEnumConverter<OpdbChangelogAction>))]
        public OpdbChangelogAction Action { get; set; }

        /// <summary>
        /// The id that is no longer valid.
        /// </summary>
        [JsonPropertyName("opdbIdDeleted")]
        public string OpdbIdDeleted { get; set; }

        /// <summary>
        /// The id to use instead. Null when the action is <see cref="OpdbChangelogAction.Delete"/>.
        /// </summary>
        [JsonPropertyName("opdbIdReplacement")]
        public string OpdbIdReplacement { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
