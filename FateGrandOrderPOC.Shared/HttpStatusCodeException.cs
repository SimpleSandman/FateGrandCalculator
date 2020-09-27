using System;
using System.Net;

namespace FateGrandOrderPOC.Shared
{
    public class HttpStatusCodeException : Exception
    {

        private HttpStatusCode statusCode;

        private HttpStatusCodeException(HttpStatusCode statusCode, String message) : base(message)
        {
            this.statusCode = statusCode;
        }

        public static HttpStatusCodeException of(HttpStatusCode statusCode)
        {
            return new HttpStatusCodeException(statusCode, "The client threw an exception with the status code " + statusCode);
        }

    }
}
