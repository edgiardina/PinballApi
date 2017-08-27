using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.Calendar
{
    public class CalendarEntry
    {
        [JsonProperty("calendar_id")]
        public int CalendarId { get; set; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("average_players")]
        public string AveragePlayers { get; set; }

        [JsonProperty("average_points")]
        public string AveragePoints { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime end_date { get; set; }
    }
}
