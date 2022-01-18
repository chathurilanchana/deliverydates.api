using System;
using System.Net;

namespace DeliveryDates.Api.Middleware.ErrorHandling
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCodeException(HttpStatusCode statusCode, string errorCode, string detailedError)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            DetailedError = detailedError;
        }

        public HttpStatusCode StatusCode { get; }
        public string ErrorCode { get; }
        public string DetailedError { get; }
    }
}
