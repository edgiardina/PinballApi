using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// An image attached to an OPDB entry. Returned only when the caller asks for image data.
    /// </summary>
    public class OpdbImage
    {
        /// <summary>
        /// The image identifier. It also forms the file name of each url in <see cref="Urls"/>.
        /// </summary>
        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// True when this is the lead image for the entry.
        /// </summary>
        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        /// <summary>
        /// Known values are <c>backglass</c>, <c>playfield</c>, <c>cabinet</c>, <c>closeup</c> and <c>other</c>.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("urls")]
        public OpdbImageUrls Urls { get; set; }

        [JsonPropertyName("sizes")]
        public OpdbImageSizes Sizes { get; set; }
    }
}
