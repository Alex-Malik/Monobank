using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monobank.Client
{
    public class UserInfo
    {
        [JsonConstructor]
        public UserInfo(string name, string webHookUrl, IEnumerable<Account> accounts)
        {
            Name = name;
            WebHookUrl = webHookUrl;
            Accounts = accounts;
        }

        public string Name { get; }

        public string WebHookUrl { get; }

        public IEnumerable<Account> Accounts { get; }
    }
}