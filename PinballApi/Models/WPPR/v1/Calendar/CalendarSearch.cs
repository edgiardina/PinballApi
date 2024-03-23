using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class CalendarSearch
    {
        [JsonPropertyName("calendar")]
        public List<CalendarDetails> Calendar { get; set; }

        [JsonPropertyName("total_entries")]
        public int TotalEntries { get; set; }
    }
}
