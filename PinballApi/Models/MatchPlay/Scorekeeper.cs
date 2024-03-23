using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay
{
    public class Scorekeeper
    {
        [JsonPropertyName("scorekeeperId")]
        public int ScorekeeperId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }
    }
}
