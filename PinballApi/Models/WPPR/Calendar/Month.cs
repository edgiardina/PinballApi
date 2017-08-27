using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.Calendar
{
    public class Month
    {
        [JsonProperty("date_month")]
        public DateTime DateMonth { get; set; }
    }
}
