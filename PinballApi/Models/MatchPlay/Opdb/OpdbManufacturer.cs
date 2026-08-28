using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Opdb
{
    public class OpdbManufacturer
    {
        [JsonPropertyName("manufacturerId")]
        public int ManufacturerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
    }
}
