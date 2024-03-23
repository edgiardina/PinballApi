using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentList
    {
        [JsonPropertyName("tournament")]
        public List<TournamentListItem> Tournament { get; set; }

        [JsonPropertyName("Total_Results")]
        public int TotalResults { get; set; }
    }
}
