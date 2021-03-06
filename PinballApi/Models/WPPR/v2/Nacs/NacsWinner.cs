﻿using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsWinner
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
