using NUnit.Framework;
using PinballApi.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    /// <summary>
    /// The 429 retry handler. These tests use a stub inner handler, so they never touch the network.
    /// </summary>
    [TestFixture]
    internal class RateLimitRetryHandlerTestFixture
    {
        private const int TooManyRequests = 429;

        /// <summary>
        /// Returns the queued responses in order and counts the calls.
        /// </summary>
        private class StubHandler : HttpMessageHandler
        {
            private readonly Queue<HttpResponseMessage> responses;

            public StubHandler(params HttpResponseMessage[] responses)
            {
                this.responses = new Queue<HttpResponseMessage>(responses);
            }

            public int CallCount { get; private set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                CallCount++;

                return Task.FromResult(responses.Count > 0
                    ? responses.Dequeue()
                    : new HttpResponseMessage(HttpStatusCode.OK));
            }
        }

        private static HttpResponseMessage RateLimited(string resetHeaderValue = null, int? retryAfterSeconds = null)
        {
            var response = new HttpResponseMessage((HttpStatusCode)TooManyRequests);

            if (resetHeaderValue != null)
            {
                response.Headers.TryAddWithoutValidation("x-ratelimit-reset", resetHeaderValue);
            }

            if (retryAfterSeconds.HasValue)
            {
                response.Headers.TryAddWithoutValidation("Retry-After", retryAfterSeconds.Value.ToString());
            }

            return response;
        }

        private static async Task<(HttpResponseMessage Response, int Calls)> Send(RateLimitRetryHandler handler, StubHandler stub)
        {
            handler.InnerHandler = stub;

            using (var invoker = new HttpMessageInvoker(handler))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://example.test/");
                var response = await invoker.SendAsync(request, CancellationToken.None);

                return (response, stub.CallCount);
            }
        }

        [Test]
        public async Task RateLimitRetryHandler_ShouldRetryAndSucceed()
        {
            var stub = new StubHandler(RateLimited(retryAfterSeconds: 1), new HttpResponseMessage(HttpStatusCode.OK));

            var (response, calls) = await Send(new RateLimitRetryHandler(2), stub);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(calls, Is.EqualTo(2));
        }

        [Test]
        public async Task RateLimitRetryHandler_ShouldStopAtMaxRetries()
        {
            var stub = new StubHandler(
                RateLimited(retryAfterSeconds: 1),
                RateLimited(retryAfterSeconds: 1),
                RateLimited(retryAfterSeconds: 1));

            var (response, calls) = await Send(new RateLimitRetryHandler(1), stub);

            Assert.That((int)response.StatusCode, Is.EqualTo(TooManyRequests), "the last 429 reaches the caller");
            Assert.That(calls, Is.EqualTo(2), "one attempt plus one retry");
        }

        [Test]
        public async Task RateLimitRetryHandler_WithZeroRetries_ShouldNotRetry()
        {
            var stub = new StubHandler(RateLimited(retryAfterSeconds: 1));

            var (response, calls) = await Send(new RateLimitRetryHandler(0), stub);

            Assert.That((int)response.StatusCode, Is.EqualTo(TooManyRequests));
            Assert.That(calls, Is.EqualTo(1));
        }

        [Test]
        public async Task RateLimitRetryHandler_ShouldNotRetryASuccess()
        {
            var stub = new StubHandler(new HttpResponseMessage(HttpStatusCode.OK));

            var (response, calls) = await Send(new RateLimitRetryHandler(3), stub);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(calls, Is.EqualTo(1));
        }

        [Test]
        public async Task RateLimitRetryHandler_ShouldReadUnixTimestampResetHeader()
        {
            // MatchPlay sends x-ratelimit-reset as a Unix timestamp in seconds.
            var resetAt = DateTimeOffset.UtcNow.AddSeconds(1).ToUnixTimeSeconds().ToString();

            var stub = new StubHandler(RateLimited(resetAt), new HttpResponseMessage(HttpStatusCode.OK));

            var (response, calls) = await Send(new RateLimitRetryHandler(2), stub);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(calls, Is.EqualTo(2));
        }

        [Test]
        public async Task RateLimitRetryHandler_ShouldGiveUpWhenTheWaitIsTooLong()
        {
            var resetAt = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds().ToString();

            var stub = new StubHandler(RateLimited(resetAt), new HttpResponseMessage(HttpStatusCode.OK));

            var (response, calls) = await Send(new RateLimitRetryHandler(3, TimeSpan.FromSeconds(5)), stub);

            Assert.That((int)response.StatusCode, Is.EqualTo(TooManyRequests), "a wait beyond the cap returns the 429");
            Assert.That(calls, Is.EqualTo(1));
        }

        [Test]
        public async Task RateLimitRetryHandler_WithoutTimingHeaders_ShouldNotRetry()
        {
            var stub = new StubHandler(RateLimited(), new HttpResponseMessage(HttpStatusCode.OK));

            var (response, calls) = await Send(new RateLimitRetryHandler(3), stub);

            Assert.That((int)response.StatusCode, Is.EqualTo(TooManyRequests), "with no wait to honour, do not spin");
            Assert.That(calls, Is.EqualTo(1));
        }

        [Test]
        public void RateLimitRetryHandler_ShouldRejectANegativeCount()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RateLimitRetryHandler(-1));
        }
    }
}
