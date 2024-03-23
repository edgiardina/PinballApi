using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class MaxMatchplay
    {
        [JsonPropertyName("games")]
        public List<TournamentGame> Games { get; set; }

        //TODO: Max Matchplay player different than regular player
        [JsonPropertyName("players")]
        public List<Player> Players { get; set; }

        [JsonPropertyName("remainingGames")]
        public int RemainingGames { get; set; }

        [JsonPropertyName("avgGameDuration")]
        public int AvgGameDuration { get; set; }
    }
}
