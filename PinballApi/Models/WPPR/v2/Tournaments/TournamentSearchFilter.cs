using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentSearchFilter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("ranking_type")]
        public RankingType? RankingType { get; set; }
    }
}
