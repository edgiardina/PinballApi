using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Card
    {
        [JsonPropertyName("cardId")]
        public int CardId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("bestGame")]
        public bool BestGame { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("singlePlayerGames")]
        public List<SinglePlayerGame> SinglePlayerGames { get; set; }

        [JsonPropertyName("singlePlayerGameIds")]
        public List<int> SinglePlayerGameIds { get; set; }

        [JsonPropertyName("arenaIds")]
        public List<int> ArenaIds { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }
    }
}
