using PinballApi.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.Universal.Tournaments.Search
{
    public class Tournament
    {
        [JsonPropertyName("tournament_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_type")]
        public string EventType { get; set; }

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

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateTimeOffset EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTimeOffset EventEndDate { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("raw_address")]
        public string RawAddress { get; set; }

        [JsonPropertyName("private_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool PrivateFlag { get; set; }

        [JsonPropertyName("ranking_system")]
        [JsonConverter(typeof(RankingSystemConverter))]
        public TournamentType RankingSystem { get; set; }

        [JsonPropertyName("preregistration_date")]
        [JsonConverter(typeof(NullableDateConverter))]
        public DateTimeOffset? PreregistrationDate { get; set; }

        [JsonPropertyName("player_count")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerCount { get; set; }

        [JsonPropertyName("qualifying_format")]
        public string QualifyingFormat { get; set; }

        [JsonPropertyName("finals_format")]
        public string FinalsFormat { get; set; }

        [JsonPropertyName("director_name")]
        public string DirectorName { get; set; }

        [JsonPropertyName("website")]
        public Uri Website { get; set; }

        [JsonPropertyName("director_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long DirectorId { get; set; }

        [JsonPropertyName("winner")]
        public TournamentWinner Winner { get; set; }

        [JsonPropertyName("certified_flag")]
        public bool CertifiedFlag { get; set; }
    }
}
