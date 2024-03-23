using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerVersusPlayerComparisonRecord
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

        [JsonPropertyName("finish_position")]
        public FinishPosition FinishPosition { get; set; }
    }
}
