using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Suggestion
    {
        [JsonPropertyName("suggestionId")]
        public int SuggestionId { get; set; }

        [JsonPropertyName("gameId")]
        public int GameId { get; set; }

        [JsonPropertyName("roundId")]
        public int RoundId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("results")]
        public List<int> Results { get; set; }

        [JsonPropertyName("arenaId")]
        public int? ArenaId { get; set; }

        [JsonPropertyName("scores")]
        public List<ulong?> Scores { get; set; }
    }
}
