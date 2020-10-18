using System;

namespace Monobank.Exceptions
{
    /// <summary>
    /// Represents error which occurs when period given to request statement is longer then 31 day and 1 hour.
    /// </summary>
    public class InvalidStatementPeriodException : MonobankException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidStatementPeriodException"/> class.
        /// </summary>
        internal InvalidStatementPeriodException() : base("The statement cannot be requested for period longer then 31 day and 1 hour.")
        {
        }
    }
}