using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi.Http
{
    /// <summary>
    /// Waits and sends the request again when the server answers HTTP 429.
    /// </summary>
    /// <remarks>
    /// MatchPlay limits most endpoints to 120 requests per minute and several, such as search and
    /// the tournament summaries, to 6 per minute. It reports the state in the
    /// <c>x-ratelimit-limit</c>, <c>x-ratelimit-remaining</c> and <c>x-ratelimit-reset</c> headers.
    /// This handler reads <c>Retry-After</c> first and falls back to <c>x-ratelimit-reset</c>.
    /// </remarks>
    public class RateLimitRetryHandler : DelegatingHandler
    {
        private const int TooManyRequests = 429;

        private readonly int maxRetries;
        private readonly TimeSpan maxDelay;

        /// <param name="maxRetries">How many extra attempts to make. Zero disables the handler.</param>
        /// <param name="maxDelay">Give up when the server asks for a longer wait than this.</param>
        public RateLimitRetryHandler(int maxRetries, TimeSpan? maxDelay = null)
        {
            if (maxRetries < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxRetries), "The count cannot be negative.");
            }

            this.maxRetries = maxRetries;
            this.maxDelay = maxDelay ?? TimeSpan.FromSeconds(90);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            for (var attempt = 0; ; attempt++)
            {
                var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if ((int)response.StatusCode != TooManyRequests || attempt >= maxRetries || !CanRepeat(request))
                {
                    return response;
                }

                var delay = GetDelay(response);

                if (delay <= TimeSpan.Zero || delay > maxDelay)
                {
                    return response;
                }

                response.Dispose();

                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// A request body can only go out twice when the content sits in memory.
        /// </summary>
        private static bool CanRepeat(HttpRequestMessage request)
        {
            return request.Content == null || request.Content is ByteArrayContent;
        }

        private static TimeSpan GetDelay(HttpResponseMessage response)
        {
            var retryAfter = response.Headers.RetryAfter;

            if (retryAfter != null)
            {
                if (retryAfter.Delta.HasValue)
                {
                    return retryAfter.Delta.Value;
                }

                if (retryAfter.Date.HasValue)
                {
                    return retryAfter.Date.Value - DateTimeOffset.UtcNow;
                }
            }

            return GetDelayFromResetHeader(response);
        }

        /// <summary>
        /// Reads <c>x-ratelimit-reset</c>. MatchPlay sends a Unix timestamp in seconds, but some
        /// servers send a count of seconds to wait. Treat a small number as a count of seconds.
        /// </summary>
        private static TimeSpan GetDelayFromResetHeader(HttpResponseMessage response)
        {
            if (!response.Headers.TryGetValues("x-ratelimit-reset", out var values))
            {
                return TimeSpan.Zero;
            }

            foreach (var value in values)
            {
                if (!long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number))
                {
                    continue;
                }

                // A Unix timestamp for any recent date is far larger than any sane wait in seconds.
                var isTimestamp = number > 1_000_000_000;

                var delay = isTimestamp
                    ? DateTimeOffset.FromUnixTimeSeconds(number) - DateTimeOffset.UtcNow
                    : TimeSpan.FromSeconds(number);

                // Add a moment so the window has certainly rolled over when the retry lands.
                return delay + TimeSpan.FromSeconds(1);
            }

            return TimeSpan.Zero;
        }
    }
}
