using Newtonsoft.Json;

namespace PinballApi.Models.WPPR
{
    public class Event
    {
        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_date")]
        public string EventDate { get; set; }

        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("original_points")]
        public string OriginalPoints { get; set; }

        [JsonProperty("current_points")]
        public string CurrentPoints { get; set; }
    }
}
