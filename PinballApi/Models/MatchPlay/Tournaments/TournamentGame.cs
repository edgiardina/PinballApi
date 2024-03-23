using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class TournamentGame : Game
    {
        [JsonPropertyName("players")]
        public List<Player> Players { get; set; }

        [JsonPropertyName("playerIds")]
        public List<int> PlayerIds { get; set; }

        [JsonPropertyName("userIds")]
        public List<int?> UserIds { get; set; }

        [JsonPropertyName("resultPositions")]
        public List<int?> ResultPositions { get; set; }

        [JsonPropertyName("resultPoints")]
        public List<float?> ResultPoints { get; set; }

        [JsonPropertyName("resultScores")]
        public List<ulong?> ResultScores { get; set; }
    }
}
