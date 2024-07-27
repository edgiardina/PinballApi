using PinballApi.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public partial class SeriesRank
    {
        [JsonPropertyName("series_code")]
        public SeriesCode SeriesCode { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("year")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long Year { get; set; }

        [JsonPropertyName("total_points")]
        public string TotalPoints { get; set; }

        [JsonPropertyName("series_rank")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long Rank { get; set; }
    }
}
