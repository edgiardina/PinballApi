using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarDistanceSearch
    {
        
        [JsonPropertyName("search_filter")]
        public CalendarDistanceSearchFilter SearchFilter { get; set; }

        [JsonPropertyName("calendar")]
        public List<CalendarEntry> CalendarEntries { get; set; }
    }
}
