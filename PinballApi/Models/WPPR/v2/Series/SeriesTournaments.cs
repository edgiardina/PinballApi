using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesTournaments
    {
        [JsonPropertyName("series_code")]
        public string SeriesCode { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("submitted_tournaments")]
        public List<SubmittedTournament> SubmittedTournaments { get; set; }

        [JsonPropertyName("unsubmitted_tournaments")]
        public List<UnsubmittedTournament> UnsubmittedTournaments { get; set; }

        [JsonPropertyName("future_tournament")]
        public List<FutureTournament> FutureTournament { get; set; }
    }
}
