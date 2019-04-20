using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarSearch
    {
        [JsonProperty("search_filter")]
        public SearchFilter SearchFilter { get; set; }

        [JsonProperty("calendar")]
        public List<CalendarEntry> CalendarEntries { get; set; }
    }
}
