using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Tournaments.Related
{
    public class RelatedTournament
    {
        [JsonPropertyName("tournament_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_type")]
        public string TournamentType { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateOnly EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateOnly EventEndDate { get; set; }

        [JsonPropertyName("ranking_system")]
        [JsonConverter(typeof(RankingSystemConverter))]
        public TournamentType RankingSystem { get; set; }

        [JsonPropertyName("winner")]
        [JsonConverter(typeof(EmptyStringToNullConverter<RelatedTournamentWinner>))]
        public RelatedTournamentWinner Winner { get; set; }
    }
}
