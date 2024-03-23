using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Standing
    {
        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("points")]
        public float Points { get; set; }

        [JsonPropertyName("pointsWithTiebreaker")]
        public float? PointsWithTiebreaker { get; set; }

        [JsonPropertyName("gamesPlayed")]
        public int GamesPlayed { get; set; }

        [JsonPropertyName("strikeCount")]
        public int? StrikeCount { get; set; }

        [JsonPropertyName("adjustment")]
        public int? Adjustment { get; set; }

        [JsonPropertyName("frenzyWins")]
        public int? FrenzyWins { get; set; }

        [JsonPropertyName("frenzyLosses")]
        public int? FrenzyLosses { get; set; }

        [JsonPropertyName("tiebreakers")]
        public List<float> Tiebreakers { get; set; }

        [JsonPropertyName("activeGames")]
        public List<string> ActiveGames { get; set; }

        [JsonPropertyName("activeGameColor")]
        public string ActiveGameColor { get; set; }
    }
}
