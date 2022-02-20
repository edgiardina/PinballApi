using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesPlayerCard
    {
        [JsonProperty("series_code")]
        public string SeriesCode { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("player_name")]
        public string PlayerName { get; set; }

        [JsonProperty("player_card")]
        public List<PlayerCard> PlayerCard { get; set; }
    }
}
