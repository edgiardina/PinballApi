using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentEvent
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonProperty("event_end_date")]
        public DateTime EventEndDate { get; set; }
    }
}