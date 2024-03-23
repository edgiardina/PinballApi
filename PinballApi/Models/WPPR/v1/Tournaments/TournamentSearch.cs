using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentSearch
    {
        [JsonPropertyName("search")]
        public string Search { get; set; }

        [JsonPropertyName("tournament")]
        public List<TournamentSearchItem> Tournament { get; set; }
    }
}
