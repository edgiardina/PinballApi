using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class Tournament
    {
        [JsonPropertyName("tournament_id")]        
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_type")]
        public string TournamentType { get; set; }

        [JsonPropertyName("prestige_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool PrestigeFlag { get; set; }

        [JsonPropertyName("private_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool PrivateFlag { get; set; }

        [JsonPropertyName("address1")]
        public string Address1 { get; set; }

        [JsonPropertyName("address2")]
        public string Address2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("intitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("contact_name")]
        public string ContactName { get; set; }

        [JsonPropertyName("website")]
        public Uri Website { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonPropertyName("event_weight")]
        public double EventWeight { get; set; }

        [JsonPropertyName("ratings_strength")]
        public double RatingsStrength { get; set; }

        [JsonPropertyName("rankings_strength")]
        public double RankingsStrength { get; set; }

        [JsonPropertyName("base_value")]
        public double BaseValue { get; set; }

        [JsonPropertyName("tournament_percentage_grade")]
        public double TournamentPercentageGrade { get; set; }

        [JsonPropertyName("tournament_value")]
        public double TournamentValue { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("games_to_win")]
        public int GamesToWin { get; set; }

        [JsonPropertyName("qualify_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool QualifyFlag { get; set; }

        [JsonPropertyName("qualify_hours")]
        public int QualifyHours { get; set; }

        [JsonPropertyName("unlimited_qualifying_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool UnlimitedQualifyingFlag { get; set; }

        [JsonPropertyName("eligible_player_count")]
        public int EligiblePlayerCount { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("ranking_system")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RankingType RankingType { get; set; }

        [JsonPropertyName("event_count")]
        public int EventCount { get; set; }

        [JsonPropertyName("events")]
        public List<TournamentEvent> Events { get; set; }
    }
}
