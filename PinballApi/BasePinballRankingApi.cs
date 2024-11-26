using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using PinballApi.Models.WPPR;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinballApi
{
    public abstract class BasePinballRankingApi
    {
        protected readonly string ApiKey;
        protected abstract PinballRankingApiVersion ApiVersion { get; }

        protected readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false)
            }
        };

        protected virtual IFlurlRequest BaseRequest => $"https://api.ifpapinball.com/{ApiVersion}/"
                                      .SetQueryParams(new { api_key = ApiKey })
                                      .WithSettings(settings =>
                                      {
                                          settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions);
                                      });

        public BasePinballRankingApi(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentOutOfRangeException("apiKey", "apiKey cannot be null or empty.");

            ApiKey = apiKey;
        }
    }
}
