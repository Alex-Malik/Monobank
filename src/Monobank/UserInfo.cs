using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monobank
{
    /// <summary>
    /// Represents the information about a user and the list of the user accounts.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfo"/> class.
        /// </summary>
        /// <param name="name">The name of the user (the client).</param>
        /// <param name="webHookUrl">The webhook URL. The webhook is used to send information about new transactions.</param>
        /// <param name="accounts">The enumeration of available user accounts.</param>
        [JsonConstructor]
        internal UserInfo(string name, string webHookUrl, IEnumerable<Account> accounts)
        {
            Name = name;
            WebHookUrl = webHookUrl;
            Accounts = accounts;
        }

        /// <summary>
        /// Gets the name of the user (the client).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the webhook URL. The webhook is used to send information about new transactions.
        /// </summary>
        public string WebHookUrl { get; }

        /// <summary>
        /// Gets the enumeration of available user accounts.
        /// </summary>
        public IEnumerable<Account> Accounts { get; }
    }
}