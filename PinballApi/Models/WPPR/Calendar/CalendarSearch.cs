using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.Calendar
{
    public class CalendarSearch
    {
        [JsonProperty("calendar")]
        public List<CalendarDetails> Calendar { get; set; }

        [JsonProperty("total_entries")]
        public int TotalEntries { get; set; }
    }
}
