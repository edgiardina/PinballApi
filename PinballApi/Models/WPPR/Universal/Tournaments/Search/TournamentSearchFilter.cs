using PinballApi.Converters;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Tournaments.Search
{
    public class TournamentSearchFilter
    {
        [JsonPropertyName("sort_mode")]
        [JsonConverter(typeof(CapitalizedEnumConverter<TournamentSearchSortMode>))]
        public TournamentSearchSortMode? SortMode { get; set; }

        [JsonPropertyName("sort_order")]
        [JsonConverter(typeof(TournamentSearchSortOrderConverter))]
        public TournamentSearchSortOrder? SortOrder { get; set; }

        [JsonPropertyName("distance_unit")]
        public string DistanceUnit { get; set; }

        [JsonPropertyName("radius")]
        public long Radius { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("event_type")]
        [JsonConverter(typeof(CapitalizedEnumConverter<TournamentEventType>))]
        public TournamentEventType? EventType { get; set; }
    }
}
