using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class TournamentWinner
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
