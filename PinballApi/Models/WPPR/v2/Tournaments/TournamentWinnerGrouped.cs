using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentWinnerGrouped
    {        
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("profile_photo", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ProfilePhoto { get; set; }

        [JsonProperty("events")]
        public List<TournamentEvent> Events { get; set; }
    }
}
