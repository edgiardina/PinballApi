using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Round
    {
        [JsonProperty("roundId")]
        public int RoundId { get; set; }

        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("arenaId")]
        public int? ArenaId { get; set; }

        [JsonProperty("score")]
        public ulong? Score { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("completedAt")]
        public DateTime? CompletedAt { get; set; }

        [JsonProperty("games")]
        public List<MatchplayGames> Games { get; set; }
    }
}
