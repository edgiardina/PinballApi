using PinballApi.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.Universal.Tournaments.Search
{
    public class TournamentSearchFilter
    {
        [JsonPropertyName("sort_mode")]
        public string SortMode { get; set; }

        [JsonPropertyName("sort_order")]
        public string SortOrder { get; set; }

        [JsonPropertyName("distance_unit")]
        public string DistanceUnit { get; set; }

        [JsonPropertyName("radius")]
        public long Radius { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
