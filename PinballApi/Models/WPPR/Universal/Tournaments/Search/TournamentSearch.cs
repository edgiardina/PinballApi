using PinballApi.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.Universal.Tournaments.Search
{
    public class TournamentSearch
    {
        [JsonPropertyName("search_filter")]
        public TournamentSearchFilter SearchFilter { get; set; }

        [JsonPropertyName("total_results")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TotalResults { get; set; }

        [JsonPropertyName("tournaments")]
        public Tournament[] Tournaments { get; set; }
    }
}
