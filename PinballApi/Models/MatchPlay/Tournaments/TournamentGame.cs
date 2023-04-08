using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class TournamentGame : Game
    {
        [JsonProperty("players")]
        public List<Player> Players { get; set; }

        [JsonProperty("playerIds")]
        public List<int> PlayerIds { get; set; }

        [JsonProperty("userIds")]
        public List<int?> UserIds { get; set; }

        [JsonProperty("resultPositions")]
        public List<int?> ResultPositions { get; set; }

        [JsonProperty("resultPoints")]
        public List<float?> ResultPoints { get; set; }

        [JsonProperty("resultScores")]
        public List<long?> ResultScores { get; set; }
    }
}
