using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentListItem
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }
        
        [JsonPropertyName("winner_name")]
        public string WinnerName { get; set; }

        [JsonPropertyName("winner_player_id")]
        public int WinnerPlayerId { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }
    }
}
