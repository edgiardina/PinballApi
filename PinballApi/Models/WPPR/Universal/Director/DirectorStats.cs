using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Director
{
    public class DirectorStats
    {
        [JsonPropertyName("unique_location_count")]
        public int UniqueLocationCount { get; set; }

        [JsonPropertyName("women_tournament_count")]
        public int WomenTournamentCount { get; set; }

        [JsonPropertyName("league_count")]
        public int LeagueCount { get; set; }

        [JsonPropertyName("highest_value")]
        public double HighestValue { get; set; }

        [JsonPropertyName("average_value")]
        public double AverageValue { get; set; }

        [JsonPropertyName("total_player_count")]
        public int TotalPlayerCount { get; set; }

        [JsonPropertyName("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonPropertyName("first_time_player_count")]
        public int FirstTimePlayerCount { get; set; }

        [JsonPropertyName("repeat_player_count")]
        public int RepeatPlayerCount { get; set; }

        [JsonPropertyName("largest_event_count")]
        public int LargestEventCount { get; set; }

        [JsonPropertyName("single_format_count")]
        public int SingleFormatCount { get; set; }

        [JsonPropertyName("multiple_format_count")]
        public int MultipleFormatCount { get; set; }

        [JsonPropertyName("unknown_format_count")]
        public int UnknownFormatCount { get; set; }

        [JsonPropertyName("last_event_date")]
        public DateOnly LastEventDate { get; set; }

        [JsonPropertyName("formats")]
        public List<Formats> Formats { get; set; }
    }
}
