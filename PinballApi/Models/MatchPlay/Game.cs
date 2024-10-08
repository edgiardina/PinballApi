﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class Game
    {
        [JsonPropertyName("gameId")]
        public int GameId { get; set; }

        [JsonPropertyName("roundId")]
        public int RoundId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("arenaId")]
        public int? ArenaId { get; set; }

        [JsonPropertyName("bankId")]
        public int? BankId { get; set; }

        [JsonPropertyName("index")]
        public int? Index { get; set; }

        [JsonPropertyName("set")]
        public int Set { get; set; }

        [JsonPropertyName("playerIdAdvantage")]
        public int? PlayerIdAdvantage { get; set; }

        [JsonPropertyName("scorekeeperId")]
        public int? ScorekeeperId { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GameStatus Status { get; set; }

        [JsonPropertyName("startedAt")]
        public DateTime? StartedAt { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonPropertyName("bye")]
        public bool Bye { get; set; }        

        [JsonPropertyName("playerIds")]
        public List<int> PlayerIds { get; set; }

        [JsonPropertyName("userIds")]
        public List<int?> UserIds { get; set; }

        [JsonPropertyName("resultPositions")]
        public List<int?> ResultPositions { get; set; }

        [JsonPropertyName("resultPoints")]
        public List<string> ResultPoints { get; set; }

        [JsonPropertyName("resultScores")]
        public List<object> ResultScores { get; set; }

        [JsonPropertyName("arena")]
        public Arena Arena { get; set; }

        [JsonPropertyName("suggestions")]
        public List<object> Suggestions { get; set; }
    }

}
