using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// The PinTips for one machine, with a short summary of the machine itself.
    /// </summary>
    public class PinTipsResult
    {
        /// <summary>
        /// The tips, ordered from most votes to fewest.
        /// </summary>
        [JsonPropertyName("pintips")]
        public List<PinTip> PinTips { get; set; }

        [JsonPropertyName("opdbInfo")]
        public OpdbInfo OpdbInfo { get; set; }
    }
}
