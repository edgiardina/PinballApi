using Newtonsoft.Json;
using PinballApi.Models.WPPR.Tournaments;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Tournaments
{
    public class TournamentSearch
    {
        [JsonProperty("search")]
        public string Search { get; set; }

        [JsonProperty("tournament")]
        public IList<Tournament> Tournaments { get; set; }
    }
}
