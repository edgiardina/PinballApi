using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesTournaments
    {
        [JsonProperty("series_code")]
        public string SeriesCode { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("submitted_tournaments")]
        public List<SubmittedTournament> SubmittedTournaments { get; set; }

        [JsonProperty("unsubmitted_tournaments")]
        public List<UnsubmittedTournament> UnsubmittedTournaments { get; set; }

        [JsonProperty("future_tournament")]
        public List<FutureTournament> FutureTournament { get; set; }
    }
}
