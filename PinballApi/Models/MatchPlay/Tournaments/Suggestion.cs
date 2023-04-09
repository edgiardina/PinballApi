using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Suggestion
    {
        [JsonProperty("suggestionId")]
        public int SuggestionId { get; set; }

        [JsonProperty("gameId")]
        public int GameId { get; set; }

        [JsonProperty("roundId")]
        public int RoundId { get; set; }

        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("playerId")]
        public int PlayerId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("results")]
        public List<int> Results { get; set; }

        [JsonProperty("arenaId")]
        public int? ArenaId { get; set; }

        [JsonProperty("scores")]
        public List<ulong?> Scores { get; set; }
    }
}
