using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsTournamentCard
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("stateprov_code")]
        public string StateProvince { get; set; }

        [JsonProperty("stateprov_name")]
        public string StateProvinceName { get; set; }

        [JsonProperty("player_name")]
        public string PlayerName { get; set; }

        [JsonProperty("nacs_tournament_card")]
        public List<NacsTournamentCardRecord> NacsTournamentCardRecords { get; set; }
    }
}
