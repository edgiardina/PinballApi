using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class RegionStanding
    {
        [JsonPropertyName("series_rank")]
        public int SeriesRank { get; set; }

        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("stateprov_code")]
        public string StateprovCode { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonPropertyName("event_count")]
        public int EventCount { get; set; }

        [JsonPropertyName("win_count")]
        public int WinCount { get; set; }
    }
}
