using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// A person credited on an OPDB entry. Returned only when the caller asks for people data.
    /// </summary>
    public class OpdbPerson
    {
        [JsonPropertyName("opdbPersonId")]
        public int OpdbPersonId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The credit for this person. Known values are <c>design</c>, <c>art</c>, <c>software</c>,
        /// <c>dots_animation</c>, <c>sound</c>, <c>mechanics</c> and <c>music</c>. OPDB adds new
        /// roles over time, so this stays a string.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// The display order of the credit on the entry.
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
