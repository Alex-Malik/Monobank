using System;

namespace Monobank.Client
{
    internal class Webhook
    {
        internal Webhook(string newHookUrl)
        {
            if (string.IsNullOrEmpty(newHookUrl))
                throw new ArgumentNullException(nameof(newHookUrl));
            NewHookUrl = newHookUrl;
        }

        public string NewHookUrl { get; }
    }
}