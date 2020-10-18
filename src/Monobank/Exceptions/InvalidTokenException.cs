﻿namespace Monobank.Exceptions
{
    /// <summary>
    /// Represents error caused by invalid Token value.
    /// </summary>
    public class InvalidTokenException : MonobankException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccountException"/> class.
        /// </summary>
        internal InvalidTokenException() : base("The token is required and cannot be the empty string.")
        {
        }
    }
}