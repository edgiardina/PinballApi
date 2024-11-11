using System.Text.Json.Serialization;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarDistanceSearchFilter
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }

        [JsonPropertyName("distance_type")]
        [JsonConverter(typeof(DistanceTypeConverter))]
        public DistanceType DistanceType { get; set; }
    }
}