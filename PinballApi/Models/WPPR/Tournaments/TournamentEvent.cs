using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.Tournaments
{
    public class TournamentEvent
    {       
        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("winner_first_name")]
        public string WinnerFirstName { get; set; }

        [JsonProperty("winner_last_name")]
        public string WinnerLastName { get; set; }

        [JsonProperty("winner_player_id")]
        public int WinnerPlayerId { get; set; }
    }
}
