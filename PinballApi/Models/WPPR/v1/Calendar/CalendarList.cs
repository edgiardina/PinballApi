using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class CalendarList
    {
        [JsonPropertyName("calendar")]
        public List<CalendarEntry> Calendar { get; set; }

        [JsonPropertyName("total_entries")]
        public int TotalEntries { get; set; }

        [JsonPropertyName("available_months")]
        public List<Month> AvailableMonths { get; set; }

        [JsonPropertyName("available_countries")]
        public List<Country> AvailableCountries { get; set; }
    }
}
