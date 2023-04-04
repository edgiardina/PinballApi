using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class TournamentArena
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("preferred")]
        public object Preferred { get; set; }

        [JsonProperty("scorbitVenuemachineId")]
        public object ScorbitVenuemachineId { get; set; }

        [JsonProperty("scorbitronInstalled")]
        public object ScorbitronInstalled { get; set; }
    }
}
