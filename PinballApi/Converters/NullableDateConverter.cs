using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi.Converters
{
    internal class NullableDateConverter : JsonConverter<DateTimeOffset?>
    {
        private const string DefaultDateTimeFormat = "yyyy-MM-dd";

        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.GetString() == "0000-00-00" || string.IsNullOrWhiteSpace(reader.GetString()))
                return null;

            return DateTimeOffset.ParseExact(reader.GetString(),
                DefaultDateTimeFormat,
                CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if(value == null)
            {
                writer.WriteStringValue("0000-00-00");
                return;
            }

            writer.WriteStringValue(value.Value.ToString(DefaultDateTimeFormat,
                CultureInfo.InvariantCulture));
        }
    }
}
