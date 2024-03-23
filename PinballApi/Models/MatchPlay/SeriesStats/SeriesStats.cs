using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.SeriesStats
{
    public class SeriesStats
    {
        [JsonPropertyName("series")]
        public Series Series { get; set; }

        [JsonPropertyName("overall")]
        public Aggregate Overall { get; set; }

        [JsonPropertyName("members")]
        public Aggregate Members { get; set; }

        [JsonPropertyName("guests")]
        public Aggregate Guests { get; set; }

        //TODO: these properties are irregularly formed
        //[JsonPropertyName("tournaments")]
        //public Tournaments Tournaments { get; set; }

        //[JsonPropertyName("playerAttendances")]
        //public PlayerAttendances PlayerAttendances { get; set; }
    }
}
