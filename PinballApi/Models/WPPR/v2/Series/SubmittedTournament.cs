using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SubmittedTournament
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonProperty("winner")]
        public TournamentWinner Winner { get; set; }

        [JsonProperty("player_count")]
        public int PlayerCount { get; set; }
    }
}
