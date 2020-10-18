namespace Monobank.Exceptions
{
    /// <summary>
    /// Represents error which occurs if call to statements API made more often then 60 seconds.
    /// </summary>
    public class ToFrequentCallException : MonobankException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToFrequentCallException"/> class.
        /// </summary>
        internal ToFrequentCallException() : base("The call to Statement API cannot be made more often then 60 seconds.")
        {
        }
    }
}