using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentEvent
    {       
        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("winner_first_name")]
        public string WinnerFirstName { get; set; }

        [JsonPropertyName("winner_last_name")]
        public string WinnerLastName { get; set; }

        [JsonPropertyName("winner_player_id")]
        public int WinnerPlayerId { get; set; }
    }
}
