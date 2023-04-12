using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class RoundStats
    {
        [JsonProperty("roundId")]
        public int RoundId { get; set; }

        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("completedAt")]
        public DateTime CompletedAt { get; set; }

        [JsonProperty("gameCount")]
        public int GameCount { get; set; }
    }
}
