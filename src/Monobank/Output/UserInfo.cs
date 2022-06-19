using System.Collections.Generic;
using System.Linq;
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
        /// <param name="clientId">the unique identifier of the client (same as for send.monobank.ua).</param>
        /// <param name="name">the name of the user (the client).</param>
        /// <param name="webHookUrl">the webhook URL (if set). It is used to send information about new transactions.</param>
        /// <param name="permissions">the string enumeration of rights provided by service (one character for one right ...whatever it means).</param>
        /// <param name="accounts">the enumeration of available user accounts (the <see cref="Account"/> objects).</param>
        /// <param name="jars">the enumeration of jars (the <see cref="Jar"/> objects).</param>
        [JsonConstructor]
        internal UserInfo(string clientId, string name, string webHookUrl, string permissions, IEnumerable<Account> accounts, IEnumerable<Jar> jars)
        {
            ClientId = clientId;
            Name = name;
            WebHookUrl = webHookUrl;
            Permissions = permissions;
            Accounts = accounts ?? Enumerable.Empty<Account>();
            Jars = jars ?? Enumerable.Empty<Jar>();
        }

        /// <summary>
        /// Gets the unique identifier of the client (same as for send.monobank.ua).
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; }
        
        /// <summary>
        /// Gets the name of the user (the client).
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Gets the webhook URL (if set). It is used to send information about new transactions.
        /// </summary>
        [JsonProperty("webHookUrl")]
        public string WebHookUrl { get; }
        
        /// <summary>
        /// Gets the string enumeration of rights provided by service (one character for one right ...whatever it means).
        /// </summary>
        [JsonProperty("permissions")]
        public string Permissions { get; }

        /// <summary>
        /// Gets the enumeration of available user accounts.
        /// </summary>
        [JsonProperty("accounts")]
        public IEnumerable<Account> Accounts { get; }

        /// <summary>
        /// Gets the enumeration of jars.
        /// </summary>
        [JsonProperty("jars")]
        public IEnumerable<Jar> Jars { get; }
    }
}