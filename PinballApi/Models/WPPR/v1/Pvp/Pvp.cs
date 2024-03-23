using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v1.Pvp
{
    public class Pvp
    {
        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_id")]
        public string TournamentId { get; set; }

        [JsonPropertyName("event_date")]
        public string EventDate { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("tournament_country_code")]
        public string TournamentCountryCode { get; set; }

        [JsonPropertyName("p1_finish_position")]
        public int P1FinishPosition { get; set; }

        [JsonPropertyName("p2_finish_position")]
        public int P2FinishPosition { get; set; }
    }
}
