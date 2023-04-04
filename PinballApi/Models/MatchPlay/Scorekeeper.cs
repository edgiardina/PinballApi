using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay
{
    public class Scorekeeper
    {
        [JsonProperty("scorekeeperId")]
        public int ScorekeeperId { get; set; }

        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
