using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentSearch
    {
        [JsonProperty("search_filter")]
        public TournamentSearchFilter SearchFilter { get; set; }

        [JsonProperty("search_count")]
        public int SearchCount { get; set; }

        [JsonProperty("tournaments")]
        public List<TournamentSearchResult> Results { get; set; }
    }
}
