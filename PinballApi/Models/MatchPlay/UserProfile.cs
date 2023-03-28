using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class UserProfile
    {
        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("rating")]
        public Rating Rating { get; set; }

        [JsonPropertyName("ifpa")]
        public Ifpa Ifpa { get; set; }

        [JsonPropertyName("shortcut")]
        public object Shortcut { get; set; }

        [JsonPropertyName("userCounts")]
        public UserCount UserCounts { get; set; }

    }
}
