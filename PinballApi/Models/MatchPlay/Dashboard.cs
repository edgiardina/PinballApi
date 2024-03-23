using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using PinballApi.Models.MatchPlay.Tournaments;

namespace PinballApi.Models.MatchPlay
{
    public class Dashboard
    {
        [JsonPropertyName("tournamentsPlaying")]
        public List<Tournament> TournamentsPlaying { get; set; }

        [JsonPropertyName("tournamentsOrganizing")]
        public List<Tournament> TournamentsOrganizing { get; set; }

        [JsonPropertyName("seriesOrganizing")]
        public List<Series> SeriesOrganizing { get; set; }

        [JsonPropertyName("rsvpTournaments")]
        public List<Tournament> RsvpTournaments { get; set; }

        [JsonPropertyName("rsvpSeries")]
        public List<Series> RsvpSeries { get; set; }
    }
}
