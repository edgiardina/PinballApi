using PinballApi.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.Universal.Tournaments
{
    public partial class Tournament
    {
        [JsonPropertyName("tournament_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TournamentId { get; set; }

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

        [JsonPropertyName("stateprov")]
        public string Stateprov { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("raw_address")]
        public string RawAddress { get; set; }

        [JsonPropertyName("director_name")]
        public string DirectorName { get; set; }

        [JsonPropertyName("director_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long DirectorId { get; set; }

        [JsonPropertyName("website")]
        public Uri Website { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateTimeOffset EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTimeOffset EventEndDate { get; set; }

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
        public double? TournamentValue { get; set; }

        [JsonPropertyName("games_to_win")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long GamesToWin { get; set; }

        [JsonPropertyName("qualify_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool QualifyFlag { get; set; }

        [JsonPropertyName("qualify_hours")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long QualifyHours { get; set; }

        [JsonPropertyName("unlimited_qualifying_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool UnlimitedQualifyingFlag { get; set; }

        [JsonPropertyName("eligible_player_count")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long EligiblePlayerCount { get; set; }

        [JsonPropertyName("player_count")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerCount { get; set; }

        [JsonPropertyName("ranking_system")]
        [JsonConverter(typeof(RankingSystemConverter))]
        public RankingSystem RankingSystem { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("qualifying_format")]
        public string QualifyingFormat { get; set; }

        [JsonPropertyName("finals_format")]
        public string FinalsFormat { get; set; }

        [JsonPropertyName("player_limit")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerLimit { get; set; }

        [JsonPropertyName("registration_date")]
        [JsonConverter(typeof(NullableDateConverter))]
        public DateTimeOffset? RegistrationDate { get; set; }

        [JsonPropertyName("matchplay_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long MatchplayId { get; set; }
    }
}
