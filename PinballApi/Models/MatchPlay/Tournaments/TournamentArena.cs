using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class TournamentArena
    {
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }

        [JsonPropertyName("preferred")]
        public object Preferred { get; set; }

        [JsonPropertyName("scorbitVenuemachineId")]
        public object ScorbitVenuemachineId { get; set; }

        [JsonPropertyName("scorbitronInstalled")]
        public object ScorbitronInstalled { get; set; }
    }
}
