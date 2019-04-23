using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerVersusPlayerComparisonRecord
    {
        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("tournament_id")]
        public string TournamentId { get; set; }

        [JsonProperty("event_date")]
        public string EventDate { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("tournament_country_code")]
        public string TournamentCountryCode { get; set; }

        [JsonProperty("finish_position")]
        public FinishPosition FinishPosition { get; set; }
    }
}
