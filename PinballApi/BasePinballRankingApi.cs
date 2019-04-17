using Flurl;
using PinballApi.Models.WPPR;
using System;

namespace PinballApi
{
    public abstract class BasePinballRankingApi
    {
        protected readonly string ApiKey;
        protected abstract PinballRankingApiVersion ApiVersion { get; }

        protected Url BaseRequest => $"https://api.ifpapinball.com/{ApiVersion}/".SetQueryParams(new { api_key = ApiKey });

        public BasePinballRankingApi(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentOutOfRangeException("apiKey", "apiKey cannot be null or empty.");

            ApiKey = apiKey;
        }
    }
}
