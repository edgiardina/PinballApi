using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class Month
    {
        [JsonPropertyName("date_month")]
        public DateTime DateMonth { get; set; }
    }
}
