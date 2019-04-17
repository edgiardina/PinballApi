using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentSearch
    {
        [JsonProperty("search")]
        public string Search { get; set; }

        [JsonProperty("tournament")]
        public List<TournamentSearchItem> Tournament { get; set; }
    }
}
