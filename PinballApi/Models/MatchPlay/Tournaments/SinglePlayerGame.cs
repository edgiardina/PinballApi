using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class SinglePlayerGame
    {
        [JsonProperty("singlePlayerGameId")]
        public int SinglePlayerGameId { get; set; }

        [JsonProperty("arenaId")]
        public int ArenaId { get; set; }

        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("playerId")]
        public int PlayerId { get; set; }

        [JsonProperty("scorekeeperId")]
        public int ScorekeeperId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("points")]
        public float Points { get; set; }

        [JsonProperty("score")]
        public ulong Score { get; set; }

        [JsonProperty("bestGame")]
        public bool BestGame { get; set; }

        [JsonProperty("index")]
        public int? Index { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
