using Newtonsoft.Json;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay
{
    public class ParentTournament
    {
        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public TournamentStatus Status { get; set; }
    }
}
