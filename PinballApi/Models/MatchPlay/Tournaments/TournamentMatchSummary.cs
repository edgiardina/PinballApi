using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    /// <summary>
    /// How one player did against one opponent on one arena in a tournament.
    /// </summary>
    /// <remarks>
    /// MatchPlay builds this summary only after the tournament is complete. The data is duplicated
    /// on purpose. If player A played player B on arena X, the result holds both an A-B-X entry
    /// and a B-A-X entry.
    /// </remarks>
    public class TournamentMatchSummary
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

        [JsonPropertyName("opponentId")]
        public int OpponentId { get; set; }

        [JsonPropertyName("tournamentDate")]
        public DateTime TournamentDate { get; set; }

        [JsonPropertyName("games")]
        public int Games { get; set; }

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
