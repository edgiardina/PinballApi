using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class Tournament
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}
