using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Calendar
{
    public class CalenderItem
    {
        [JsonProperty("calendar")]
        public List<CalendarDetails> Calendar { get; set; }

        [JsonProperty("all_dates")]
        public List<Date> AllDates { get; set; }
    }
}
