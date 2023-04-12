using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class MatchplayStats
    {
        [JsonProperty("avgGamesPerPlayer")]
        public int AvgGamesPerPlayer { get; set; }

        [JsonProperty("totalGames")]
        public int TotalGames { get; set; }
    }
}