using System;

namespace Monobank.Exceptions
{
    /// <summary>
    /// Represents error caused by invalid account value. It could be null value or empty string.
    /// </summary>
    public class InvalidAccountException : MonobankException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccountException"/> class.
        /// </summary>
        internal InvalidAccountException() : base("The account is required and expected to be not the empty string value.")
        {
        }
    }
}