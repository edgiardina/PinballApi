﻿using System.Text.Json.Serialization;
using System.Collections.Generic;
using PinballApi.Models.WPPR.v2.Tournaments;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CountryRanking
    {
        [JsonPropertyName("ranking_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RankingType RankingType { get; set; }

        [JsonPropertyName("start_position")]
        public int StartPosition { get; set; }

        [JsonPropertyName("return_count")]
        public int ReturnCount { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("rank_country_name")]
        public string RankCountryName { get; set; }

        [JsonPropertyName("rankings")]
        public List<CountryRankingResult> Rankings { get; set; }
    }
}
