using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Other
{
    public class CountryDetail
    {
        [JsonPropertyName("country_id")]
        public string CountryId { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("active_flag")]
        public string ActiveFlag { get; set; }

        public bool IsActive => ActiveFlag == "Y";
    }
}
