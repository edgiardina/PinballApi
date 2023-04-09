using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Standing
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("points")]
        public float Points { get; set; }

        [JsonProperty("pointsWithTiebreaker")]
        public float? PointsWithTiebreaker { get; set; }

        [JsonProperty("gamesPlayed")]
        public int GamesPlayed { get; set; }

        [JsonProperty("strikeCount")]
        public int? StrikeCount { get; set; }

        [JsonProperty("adjustment")]
        public int? Adjustment { get; set; }

        [JsonProperty("frenzyWins")]
        public int? FrenzyWins { get; set; }

        [JsonProperty("frenzyLosses")]
        public int? FrenzyLosses { get; set; }

        [JsonProperty("tiebreakers")]
        public List<float> Tiebreakers { get; set; }

        [JsonProperty("activeGames")]
        public List<string> ActiveGames { get; set; }

        [JsonProperty("activeGameColor")]
        public string ActiveGameColor { get; set; }
    }
}
