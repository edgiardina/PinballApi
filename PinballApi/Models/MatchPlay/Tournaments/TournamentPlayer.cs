using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class TournamentPlayer
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("seed")]
        public object Seed { get; set; }

        [JsonProperty("pointsAdjustment")]
        public int PointsAdjustment { get; set; }

        [JsonProperty("subscription")]
        public object Subscription { get; set; }
    }
}
