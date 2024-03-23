using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PinballApi.Converters;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Round
    {
        [JsonPropertyName("roundId")]
        public int RoundId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("arenaId")]
        public int? ArenaId { get; set; }

        [JsonPropertyName("score")]
        public ulong? Score { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonPropertyName("createdAt")]
        [JsonConverter(typeof(NonISODateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("completedAt")]
        [JsonConverter(typeof(NonISODateTimeConverter))]
        public DateTime? CompletedAt { get; set; }

        [JsonPropertyName("games")]
        public List<MatchplayGames> Games { get; set; }
    }
}
