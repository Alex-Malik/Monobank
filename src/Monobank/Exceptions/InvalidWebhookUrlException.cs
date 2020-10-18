namespace Monobank.Exceptions
{
    /// <summary>
    /// Represents error caused by invalid Webhook URL. It could be null value or empty string.
    /// </summary>
    public class InvalidWebhookUrlException : MonobankException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidWebhookUrlException"/> class.
        /// </summary>
        internal InvalidWebhookUrlException() : base("The webhook URL is required and cannot be the empty string.")
        {
        }
    }
}