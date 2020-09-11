using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Monobank.Client
{
    /// <summary>
    /// Provides methods to communicate with Monobank's API.
    /// </summary>
    public class Monobank
    {
        private const string MonobankBaseUrl = "https://api.monobank.ua";
        private const string BankCurrencyUrlPart = "/bank/currency";
        private const string UserInfoUrlPart = "/personal/client-info";
        private const string WebhookUrlPart = "/personal/webhook";
        private const string StatementUrlPart = "/personal/statement/{account}/{from}/{to}";
        private const string XToken = "X-Token";
        private readonly string _token;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Monobank"/> class with the personal token used to authenticate a person who makes a request.
        /// </summary>
        /// <param name="token">The token used to authenticate a person.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="token"/> is null or empty string.</exception>
        public Monobank(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            // TODO Maybe make test request? 

            _token = token;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add(XToken, _token);
        }

        /// <summary>
        /// Gets a basic list of Monobank exchange rates. The server caches and updates information no more than once every 5 minutes.
        /// </summary>
        /// <returns>The list of Monobank exchange rates represented by <see cref="CurrencyInfo"/> objects.</returns>
        /// <exception cref="MonobankException"></exception>
        public async Task<IEnumerable<CurrencyInfo>> GetCurrencyRatesAsync()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(UrlForCurrencyRates());

                var json = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<CurrencyInfo>>(json);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        /// <summary>
        /// Gets a personal client's information and accounts. This function can be used only ones each 60 seconds.
        /// </summary>
        /// <returns>The <see cref="UserInfo"/> object containing client's personal information and accounts.</returns>
        /// <exception cref="MonobankException"></exception>
        public async Task<UserInfo> GetUserInfo()
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync(UrlForUserInfo());

                var json = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<UserInfo>(json);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        /// <summary>
        /// Sets the URL which will be used to send POST requests of <see cref=""/> objects.
        /// <para/> 
        /// <para/>
        /// If the user service under the URL will not respond within 5 seconds the request will be send again in 60
        /// and 600 seconds. If no response is received on the third attempt, the function will be disabled.
        /// </summary>
        /// <param name="newHookUrl">The new webhook URL to set up.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MonobankException"></exception>
        public async Task SetWebhookAsync(string newHookUrl)
        {
            if (string.IsNullOrEmpty(newHookUrl))
                throw new ArgumentNullException(newHookUrl);
            var webhook = new Webhook(newHookUrl);

            try
            {
                var response = await _httpClient.PostAsync(UrlForWebhook(), ConvertToJsonContent(webhook));
                
                // TODO Handle Errors.
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        /// <summary>
        /// Gets a statement for specified account within given period of rime.
        /// </summary>
        /// <param name="account">The unique identifier of the account. Specify "0" to get default account.</param>
        /// <param name="from">The UTC date and time to get statement from.</param>
        /// <param name="to">The UTC date and time until the statement should be loaded.</param>
        /// <returns>The list of <see cref="StatementItem"/>s.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MonobankException"></exception>
        public async Task<IEnumerable<StatementItem>> GetStatementAsync(string account, DateTime from, DateTime to)
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException(nameof(account));
            // TODO Validate difference between from and to values.

            try
            {
                var httpResponse = await _httpClient.GetAsync(UrlForStatement(account, from, to));
                
                var json = await httpResponse.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<IEnumerable<StatementItem>>(json);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        private string UrlForCurrencyRates() => MonobankBaseUrl + BankCurrencyUrlPart;

        private string UrlForUserInfo() => MonobankBaseUrl + UserInfoUrlPart;

        private string UrlForWebhook() => MonobankBaseUrl + WebhookUrlPart;

        private string UrlForStatement(string account, DateTime from, DateTime to)
        {
            return MonobankBaseUrl + StatementUrlPart
                .Replace("{account}", account)
                .Replace("{from}", new DateTimeOffset(from).ToUnixTimeSeconds().ToString())
                .Replace("{to}", new DateTimeOffset(to).ToUnixTimeSeconds().ToString());
        }

        private HttpContent ConvertToJsonContent(object value)
        {
            //return new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            return new StringContent(JsonConvert.SerializeObject(value));
        }
    }
}