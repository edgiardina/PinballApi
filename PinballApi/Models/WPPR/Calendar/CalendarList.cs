using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Calendar
{
    public class CalendarList
    {
        [JsonProperty("calendar")]
        public List<CalendarEntry> Calendar { get; set; }

        [JsonProperty("total_entries")]
        public int TotalEntries { get; set; }

        [JsonProperty("available_months")]
        public List<Month> AvailableMonths { get; set; }

        [JsonProperty("available_countries")]
        public List<Country> AvailableCountries { get; set; }
    }
}
