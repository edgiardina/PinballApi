using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class CalendarEntry
    {
        [JsonPropertyName("calendar_id")]
        public int CalendarId { get; set; }

        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("average_players")]
        public string AveragePlayers { get; set; }

        [JsonPropertyName("average_points")]
        public string AveragePoints { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
    }
}
