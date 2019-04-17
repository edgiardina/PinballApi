using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class Month
    {
        [JsonProperty("date_month")]
        public DateTime DateMonth { get; set; }
    }
}
