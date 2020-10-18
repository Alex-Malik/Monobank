using System;

namespace Monobank
{
    /// <summary>
    /// Represents errors occured during <see cref="Monobank"/> execution. 
    /// </summary>
    public class MonobankException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonobankException"/> class with a reference to the wrapped exception.
        /// </summary>
        /// <param name="exception">The exception which is wrapped by this instance of <see cref="MonobankException"/>.</param>
        internal MonobankException(string message, Exception exception) : base(message, exception)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MonobankException"/> class with a specified error message. 
        /// </summary>
        /// <param name="exception">The error message.</param>
        internal MonobankException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Represents error caused by invalid account value. It could be null value or empty string.
    /// </summary>
    public class InvalidAccountException : MonobankException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccountException"/> class.
        /// </summary>
        internal InvalidAccountException() : base(String.Empty) {}
    }
    
    public class InvalidStatementPeriodException : MonobankException
    {
        internal InvalidStatementPeriodException() : base(String.Empty) {}
    }

    public class ToFrequentCallException : MonobankException
    {
        internal ToFrequentCallException() : base(string.Empty) {}
    }

    public class InvalidWebhookUrlException : MonobankException
    {
        internal InvalidWebhookUrlException() : base(string.Empty) {}
    }

    public class InvalidTokenException : MonobankException
    {
        internal InvalidTokenException() : base(string.Empty){}
    }
}