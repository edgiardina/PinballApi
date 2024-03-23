using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentWinner
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("profile_photo")]
        public Uri ProfilePhoto { get; set; }
    }
}
