using System.Text.Json.Serialization;
using System;
using PinballApi.Models.WPPR.v2.Tournaments;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarSearchFilter
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("ranking_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RankingType RankingType { get; set; }
    }
}