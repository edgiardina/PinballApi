using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class MatchplayGames
    {
        [JsonProperty("gameId")]
        public int GameId { get; set; }

        [JsonProperty("roundId")]
        public int RoundId { get; set; }

        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("arenaId")]
        public int ArenaId { get; set; }

        [JsonProperty("bankId")]
        public object BankId { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("set")]
        public int Set { get; set; }

        [JsonProperty("playerIdAdvantage")]
        public object PlayerIdAdvantage { get; set; }

        [JsonProperty("scorekeeperId")]
        public int ScorekeeperId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("startedAt")]
        public DateTime StartedAt { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("bye")]
        public bool Bye { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }

        [JsonProperty("playerIds")]
        public List<int> PlayerIds { get; set; }

        [JsonProperty("userIds")]
        public List<int?> UserIds { get; set; }

        [JsonProperty("resultPositions")]
        public List<int> ResultPositions { get; set; }

        [JsonProperty("resultPoints")]
        public List<float> ResultPoints { get; set; }

        [JsonProperty("resultScores")]
        public List<ulong?> ResultScores { get; set; }

        [JsonProperty("arena")]
        public Arena Arena { get; set; }

        [JsonProperty("suggestions")]
        public List<Suggestion> Suggestions { get; set; }
    }
}
