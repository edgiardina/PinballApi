using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentWinner
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonProperty("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("profile_photo", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ProfilePhoto { get; set; }
    }
}
