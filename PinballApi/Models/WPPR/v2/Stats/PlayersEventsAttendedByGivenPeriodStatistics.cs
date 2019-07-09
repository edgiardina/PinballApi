﻿using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class PlayersEventsAttendedByGivenPeriodStatistics
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
