using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class Dashboard
    {
        [JsonPropertyName("tournamentsPlaying")]
        public List<Tournament> TournamentsPlaying { get; } = new List<Tournament>();

        [JsonPropertyName("tournamentsOrganizing")]
        public List<Tournament> TournamentsOrganizing { get; } = new List<Tournament>();

        [JsonPropertyName("seriesOrganizing")]
        public List<Series> SeriesOrganizing { get; } = new List<Series>();

        [JsonPropertyName("rsvpTournaments")]
        public List<Tournament> RsvpTournaments { get; } = new List<Tournament>();

        [JsonPropertyName("rsvpSeries")]
        public List<Series> RsvpSeries { get; } = new List<Series>();
    }
}
