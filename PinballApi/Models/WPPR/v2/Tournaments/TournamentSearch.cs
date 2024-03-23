using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentSearch
    {
        [JsonPropertyName("search_filter")]
        public TournamentSearchFilter SearchFilter { get; set; }

        [JsonPropertyName("search_count")]
        public int SearchCount { get; set; }

        [JsonPropertyName("tournaments")]
        public List<TournamentSearchResult> Results { get; set; }
    }
}
