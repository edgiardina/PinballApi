using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinballApi.Converters
{
    /// <summary>
    /// Reads a date that is either ISO 8601 (<c>2015-08-05T20:28:10Z</c>) or space separated
    /// (<c>2015-08-05 20:28:10</c>). The MatchPlay API sends ISO 8601, but some of the bulk data
    /// exports still send the space separated form.
    /// </summary>
    public class FlexibleDateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly string[] Formats =
        {
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm",
            "yyyy-MM-dd"
        };

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTime(out var isoValue))
            {
                return isoValue;
            }

            var value = reader.GetString();

            if (DateTime.TryParseExact(value, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
            {
                return parsed;
            }

            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("O", CultureInfo.InvariantCulture));
        }
    }
}
