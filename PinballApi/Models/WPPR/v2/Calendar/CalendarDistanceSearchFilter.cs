using Newtonsoft.Json;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarDistanceSearchFilter
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }

        [JsonProperty("distance_type")]
        [JsonConverter(typeof(DistanceTypeConverter))]
        public DistanceType DistanceType { get; set; }
    }

    public enum DistanceType
    {
        Miles,
        Kilometers
    }
}