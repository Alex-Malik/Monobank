namespace Monobank
{
    /// <summary>
    /// Represents data which is used to set a new webhook URL for the user.
    /// </summary>
    internal class Webhook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Webhook"/> class with the given URL for the webhook.
        /// </summary>
        /// <param name="newHookUrl">A string representation of a new URL for the webhook.</param>
        internal Webhook(string newHookUrl)
        {
            NewHookUrl = newHookUrl;
        }

        /// <summary>
        /// Gets a new URL that is meant to be set as webhook for the client. 
        /// </summary>
        public string NewHookUrl { get; }
    }
}