using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Queue
    {
        [JsonPropertyName("queueId")]
        public int QueueId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("arenaId")]
        public int? ArenaId { get; set; }

        [JsonPropertyName("roundId")]
        public int? RoundId { get; set; }

        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("completedAt")]
        public DateTime? CompletedAt { get; set; }
    }
}
