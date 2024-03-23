using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class Date
    {
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }
    }
}
