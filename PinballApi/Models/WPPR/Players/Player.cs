using Newtonsoft.Json;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.Players
{
    public class Player
    {
        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("initials")]
        public string Initials { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("excluded_flag")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool ExcludedFlag { get; set; }

        [JsonProperty("ifpa_registered")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IfpaRegistered { get; set; }
    }
}
