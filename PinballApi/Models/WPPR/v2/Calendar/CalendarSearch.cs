using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public partial class CalendarSearch
    {
        [JsonPropertyName("search_distance")]
        public long SearchDistance { get; set; }

        [JsonPropertyName("distance_type")]
        public string DistanceType { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("calendar")]
        public CalendarEntry[] Calendar { get; set; }

        [JsonPropertyName("total_entries")]
        public long TotalEntries { get; set; }
    }
}
