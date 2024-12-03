using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Tournaments
{
    public class TournamentFormats
    {
        [JsonPropertyName("qualifying_formats")]
        public List<TournamentFormat> QualifyingFormats { get; set; }

        [JsonPropertyName("finals_formats")]
        public List<TournamentFormat> FinalsFormats { get; set; }
    }
}
