using System;
using System.Net;

namespace PinballApi
{
    /// <summary>
    /// A call to a pinball data service failed.
    /// </summary>
    /// <remarks>
    /// This keeps the HTTP layer out of the caller's code. Read <see cref="StatusCode"/> to tell
    /// a missing record from a rate limit, and <see cref="ResponseBody"/> for the message the
    /// service sent back. The original Flurl exception stays available as
    /// <see cref="Exception.InnerException"/>.
    /// </remarks>
    public class PinballApiException : Exception
    {
        public PinballApiException(string message, HttpStatusCode? statusCode, string responseBody, string requestUrl, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
            RequestUrl = requestUrl;
        }

        /// <summary>
        /// The status the service returned. Null when the call never got a response, for example
        /// when it timed out.
        /// </summary>
        public HttpStatusCode? StatusCode { get; }

        /// <summary>
        /// The raw body of the failed response. Null when there was none.
        /// </summary>
        public string ResponseBody { get; }

        /// <summary>
        /// The url that failed.
        /// </summary>
        public string RequestUrl { get; }

        /// <summary>
        /// True when the service refused the call because the caller went over a rate limit.
        /// Wait and try again, or build the client with a rate limit retry count.
        /// </summary>
        public bool IsRateLimited => StatusCode.HasValue && (int)StatusCode.Value == 429;

        /// <summary>
        /// True when the record does not exist.
        /// </summary>
        public bool IsNotFound => StatusCode == HttpStatusCode.NotFound;

        /// <summary>
        /// True when the token is missing, wrong, or not allowed to make this call.
        /// </summary>
        public bool IsUnauthorized => StatusCode == HttpStatusCode.Unauthorized || StatusCode == HttpStatusCode.Forbidden;
    }
}
