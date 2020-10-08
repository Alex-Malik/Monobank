﻿using System.Net.Http;

namespace Monobank
{
    /// <summary>
    /// Represents errors occured during sending a request to Monobank's API.
    /// </summary>
    public class MonobankRequestException : MonobankException
    {
        // TODO Move to resources.
        private const string ExceptionMessage =
            "The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MonobankRequestException"/> class based on the <see cref="HttpRequestException"/>.
        /// </summary>
        /// <param name="exception">The <see cref="HttpRequestException"/> occured during request.</param>
        internal MonobankRequestException(HttpRequestException exception)
            : base(ExceptionMessage, exception)
        {
        }
    }
}