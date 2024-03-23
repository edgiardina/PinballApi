using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class CalenderItem
    {
        [JsonPropertyName("calendar")]
        public List<CalendarDetails> Calendar { get; set; }

        [JsonPropertyName("all_dates")]
        public List<Date> AllDates { get; set; }
    }
}
