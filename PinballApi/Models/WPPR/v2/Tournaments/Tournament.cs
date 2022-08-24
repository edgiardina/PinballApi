using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class Tournament
    {
        [JsonProperty("tournament_id")]        
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("tournament_type")]
        public string TournamentType { get; set; }

        [JsonProperty("prestige_flag")]        
        public bool PrestigeFlag { get; set; }

        [JsonProperty("private_flag")]        
        public bool PrivateFlag { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("intitude")]
        public double Longitude { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("contact_name")]
        public string ContactName { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonProperty("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonProperty("event_weight")]
        public double EventWeight { get; set; }

        [JsonProperty("ratings_strength")]
        public double RatingsStrength { get; set; }

        [JsonProperty("rankings_strength")]
        public double RankingsStrength { get; set; }

        [JsonProperty("base_value")]
        public double BaseValue { get; set; }

        [JsonProperty("tournament_percentage_grade")]
        public double TournamentPercentageGrade { get; set; }

        [JsonProperty("tournament_value")]
        public double TournamentValue { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("games_to_win")]
        public int GamesToWin { get; set; }

        [JsonProperty("qualify_flag")]
        
        public bool QualifyFlag { get; set; }

        [JsonProperty("qualify_hours")]
        public int QualifyHours { get; set; }

        [JsonProperty("unlimited_qualifying_flag")]        
        public bool UnlimitedQualifyingFlag { get; set; }

        [JsonProperty("eligible_player_count")]
        public int EligiblePlayerCount { get; set; }

        [JsonProperty("player_count")]
        public int PlayerCount { get; set; }

        [JsonProperty("ranking_system")]
        public RankingType RankingType { get; set; }

        [JsonProperty("event_count")]
        public int EventCount { get; set; }

        [JsonProperty("events")]
        public List<TournamentEvent> Events { get; set; }
    }
}
