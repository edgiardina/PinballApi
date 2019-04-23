using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class Date
    {
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }
    }
}
