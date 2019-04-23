using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarDistanceSearch
    {
        [JsonProperty("search_filter")]
        public CalendarDistanceSearchFilter SearchFilter { get; set; }

        [JsonProperty("calendar")]
        public List<CalendarEntry> CalendarEntries { get; set; }
    }
}
