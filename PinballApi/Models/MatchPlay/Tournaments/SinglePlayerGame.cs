using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class SinglePlayerGame
    {
        [JsonPropertyName("singlePlayerGameId")]
        public int SinglePlayerGameId { get; set; }

        [JsonPropertyName("arenaId")]
        public int ArenaId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("scorekeeperId")]
        public int ScorekeeperId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("points")]
        public float Points { get; set; }

        [JsonPropertyName("score")]
        public ulong Score { get; set; }

        [JsonPropertyName("bestGame")]
        public bool BestGame { get; set; }

        [JsonPropertyName("index")]
        public int? Index { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
