using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// A short playing tip for a machine.
    /// </summary>
    public class PinTip
    {
        [JsonPropertyName("tipId")]
        public int TipId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Known values are <c>general</c>, <c>multiball</c>, <c>secret</c> and <c>skillshot</c>.
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// How many players voted the tip up.
        /// </summary>
        [JsonPropertyName("voteTotal")]
        public int VoteTotal { get; set; }

        /// <summary>
        /// True when the holder of the API token voted for this tip. The bulk export omits it.
        /// </summary>
        [JsonPropertyName("ownVote")]
        public bool OwnVote { get; set; }

        /// <summary>
        /// The OPDB id the tip belongs to. The bulk export sets this. The API does not, because
        /// the caller already knows which entry was asked for.
        /// </summary>
        [JsonPropertyName("opdbId")]
        public string OpdbId { get; set; }

        /// <summary>
        /// The bulk export sets this. The API omits it.
        /// </summary>
        [JsonPropertyName("createdAt")]
        [JsonConverter(typeof(FlexibleDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The bulk export sets this. The API omits it.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        [JsonConverter(typeof(FlexibleDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
