using System;

namespace Monobank.Client
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
        public MonobankException(Exception exception) : base(null, exception)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MonobankException"/> class with a specified error message. 
        /// </summary>
        /// <param name="exception">The error message.</param>
        public MonobankException(string message) : base(message)
        {
        }
    }
}