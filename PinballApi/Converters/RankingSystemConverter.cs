using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using PinballApi.Models.WPPR.Universal;

namespace PinballApi.Converters
{
    public class RankingSystemConverter : JsonConverter<RankingSystem>
    {
        public override RankingSystem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumString = reader.GetString().ToLower();
            enumString = UppercaseFirst(enumString);
            return (RankingSystem)Enum.Parse(typeToConvert, enumString, ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, RankingSystem value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
