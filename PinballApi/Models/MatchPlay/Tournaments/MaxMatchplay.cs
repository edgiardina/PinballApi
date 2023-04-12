using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class MaxMatchplay
    {
        [JsonProperty("games")]
        public List<TournamentGame> Games { get; set; }

        //TODO: Max Matchplay player different than regular player
        [JsonProperty("players")]
        public List<Player> Players { get; set; }

        [JsonProperty("remainingGames")]
        public int RemainingGames { get; set; }

        [JsonProperty("avgGameDuration")]
        public int AvgGameDuration { get; set; }
    }
}
