using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.SeriesStats
{
    public class SeriesStats
    {
        [JsonProperty("series")]
        public Series Series { get; set; }

        [JsonProperty("overall")]
        public Aggregate Overall { get; set; }

        [JsonProperty("members")]
        public Aggregate Members { get; set; }

        [JsonProperty("guests")]
        public Aggregate Guests { get; set; }

        //TODO: these properties are irregularly formed
        //[JsonProperty("tournaments")]
        //public Tournaments Tournaments { get; set; }

        //[JsonProperty("playerAttendances")]
        //public PlayerAttendances PlayerAttendances { get; set; }
    }
}
