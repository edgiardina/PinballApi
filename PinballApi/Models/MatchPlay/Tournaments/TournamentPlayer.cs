using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class TournamentPlayer
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("seed")]
        public object Seed { get; set; }

        [JsonPropertyName("pointsAdjustment")]
        public int PointsAdjustment { get; set; }

        [JsonPropertyName("subscription")]
        public object Subscription { get; set; }
    }
}
