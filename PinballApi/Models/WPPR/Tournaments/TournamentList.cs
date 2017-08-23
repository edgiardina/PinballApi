using Newtonsoft.Json;
using PinballApi.Models.WPPR.Tournaments;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR
{
    public class TournamentList
    {
        [JsonProperty("tournament")]
        public IList<Tournament> Tournament { get; set; }

        [JsonProperty("Total_Results")]
        public string TotalResults { get; set; }
    }
}
