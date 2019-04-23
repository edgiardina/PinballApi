using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentList
    {
        [JsonProperty("tournament")]
        public List<TournamentListItem> Tournament { get; set; }

        [JsonProperty("Total_Results")]
        public int TotalResults { get; set; }
    }
}
