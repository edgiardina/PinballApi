using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    /// <summary>
    /// How one player did on one arena in a tournament. MatchPlay builds this summary only after
    /// the tournament is complete.
    /// </summary>
    public class TournamentPlayerArenaSummary
    {
        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        /// <summary>
        /// The user who owns the tournament.
        /// </summary>
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("arenaId")]
        public int ArenaId { get; set; }

        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("tournamentDate")]
        public DateTime TournamentDate { get; set; }

        [JsonPropertyName("games")]
        public int Games { get; set; }

        [JsonPropertyName("singlePlayerGames")]
        public int SinglePlayerGames { get; set; }

        [JsonPropertyName("totalGames")]
        public int TotalGames { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("opdbGroup")]
        public string OpdbGroup { get; set; }

        [JsonPropertyName("opdbId")]
        public string OpdbId { get; set; }
    }
}
