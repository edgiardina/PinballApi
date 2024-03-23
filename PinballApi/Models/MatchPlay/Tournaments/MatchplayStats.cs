using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class MatchplayStats
    {
        [JsonPropertyName("avgGamesPerPlayer")]
        public int AvgGamesPerPlayer { get; set; }

        [JsonPropertyName("totalGames")]
        public int TotalGames { get; set; }
    }
}