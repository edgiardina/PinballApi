using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    /// <summary>
    /// How many times one arena was played in a tournament. MatchPlay builds this summary only
    /// after the tournament is complete.
    /// </summary>
    public class TournamentArenaSummary
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

        [JsonPropertyName("tournamentDate")]
        public DateTime TournamentDate { get; set; }

        /// <summary>
        /// Multi-player games played on this arena.
        /// </summary>
        [JsonPropertyName("games")]
        public int Games { get; set; }

        [JsonPropertyName("singlePlayerGames")]
        public int SinglePlayerGames { get; set; }

        [JsonPropertyName("totalGames")]
        public int TotalGames { get; set; }

        /// <summary>
        /// The OPDB group of the machine. Use it to join arenas across tournaments.
        /// </summary>
        [JsonPropertyName("opdbGroup")]
        public string OpdbGroup { get; set; }

        [JsonPropertyName("opdbId")]
        public string OpdbId { get; set; }
    }
}
