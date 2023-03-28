using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class Location
    {
        [JsonPropertyName("locationId")]
        public int LocationId { get; set; }

        [JsonPropertyName("scorbitVenueId")]
        public int? ScorbitVenueId { get; set; }

        [JsonPropertyName("pinballmapId")]
        public int? PinballMapId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("organizerId")]
        public int OrganizerId { get; set; }

        [JsonPropertyName("status")]
        public LocationStatus Status { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}