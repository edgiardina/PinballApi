using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class UnsubmittedTournament
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("director_name")]
        public string DirectorName { get; set; }
    }
}
