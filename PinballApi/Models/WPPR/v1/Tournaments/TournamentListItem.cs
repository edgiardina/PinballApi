using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentListItem
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }
        
        [JsonProperty("winner_name")]
        public string WinnerName { get; set; }

        [JsonProperty("winner_player_id")]
        public int WinnerPlayerId { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("player_count")]
        public int PlayerCount { get; set; }
    }
}
