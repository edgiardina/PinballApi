using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    /// <summary>
    /// One page of a list response, with the links MatchPlay uses to walk the pages.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    public class PagedResult<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }

        [JsonPropertyName("links")]
        public PageLinks Links { get; set; }

        [JsonPropertyName("meta")]
        public PageMeta Meta { get; set; }

        /// <summary>
        /// True when another page follows this one.
        /// </summary>
        [JsonIgnore]
        public bool HasMore => !string.IsNullOrWhiteSpace(Links?.Next);
    }

    public class PageLinks
    {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }

        [JsonPropertyName("prev")]
        public string Previous { get; set; }

        /// <summary>
        /// The url of the next page. Null on the last page.
        /// </summary>
        [JsonPropertyName("next")]
        public string Next { get; set; }
    }

    public class PageMeta
    {
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("per_page")]
        public int? PerPage { get; set; }

        [JsonPropertyName("from")]
        public int? From { get; set; }

        [JsonPropertyName("to")]
        public int? To { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}
