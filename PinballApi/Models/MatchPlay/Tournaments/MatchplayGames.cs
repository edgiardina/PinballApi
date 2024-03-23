using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class MatchplayGames
    {
        [JsonPropertyName("gameId")]
        public int GameId { get; set; }

        [JsonPropertyName("roundId")]
        public int RoundId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("arenaId")]
        public int ArenaId { get; set; }

        [JsonPropertyName("bankId")]
        public object BankId { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("set")]
        public int Set { get; set; }

        [JsonPropertyName("playerIdAdvantage")]
        public object PlayerIdAdvantage { get; set; }

        [JsonPropertyName("scorekeeperId")]
        public int ScorekeeperId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("startedAt")]
        public DateTime StartedAt { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("bye")]
        public bool Bye { get; set; }

        [JsonPropertyName("players")]
        public List<Player> Players { get; set; }

        [JsonPropertyName("playerIds")]
        public List<int> PlayerIds { get; set; }

        [JsonPropertyName("userIds")]
        public List<int?> UserIds { get; set; }

        [JsonPropertyName("resultPositions")]
        public List<int> ResultPositions { get; set; }

        [JsonPropertyName("resultPoints")]
        public List<float> ResultPoints { get; set; }

        [JsonPropertyName("resultScores")]
        public List<ulong?> ResultScores { get; set; }

        [JsonPropertyName("arena")]
        public Arena Arena { get; set; }

        [JsonPropertyName("suggestions")]
        public List<Suggestion> Suggestions { get; set; }
    }
}
