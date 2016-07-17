using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.Ranking
{
    public class TournamentSearch
    {
        [JsonProperty("search")]
        public string Search { get; set; }

        [JsonProperty("tournament")]
        public IList<Tournament> Tournament { get; set; }
    }
}
