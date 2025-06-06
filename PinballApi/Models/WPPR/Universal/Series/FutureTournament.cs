﻿using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class FutureTournament
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("director_name")]
        public string DirectorName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
