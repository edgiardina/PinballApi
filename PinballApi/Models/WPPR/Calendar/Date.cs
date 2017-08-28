using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.Calendar
{
    public class Date
    {
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }
    }
}
