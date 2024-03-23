using System.Text.Json.Serialization;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay
{
    public class ParentTournament
    {
        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public TournamentStatus Status { get; set; }
    }
}
