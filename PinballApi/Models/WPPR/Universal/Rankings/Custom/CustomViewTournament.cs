using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings.Custom
{
    public class CustomViewTournament
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_date")]
        public string EventDate { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
