using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// The machine summary that rides along with a PinTips response.
    /// </summary>
    public class OpdbInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The manufacturer and the year, for example <c>Bally, 1994</c>.
        /// </summary>
        [JsonPropertyName("supplementary")]
        public string Supplementary { get; set; }

        [JsonPropertyName("hasRuleset")]
        public bool HasRuleset { get; set; }

        [JsonPropertyName("pinballPrimerUrl")]
        public string PinballPrimerUrl { get; set; }

        [JsonPropertyName("bobsGuideUrl")]
        public string BobsGuideUrl { get; set; }

        [JsonPropertyName("pinballRulesUrl")]
        public string PinballRulesUrl { get; set; }

        [JsonPropertyName("pinballCardsUrl")]
        public string PinballCardsUrl { get; set; }
    }
}
