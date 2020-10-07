using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Monobank
{
    /// <summary>
    /// Provides methods to communicate with Monobank's API.
    /// </summary>
    public class Monobank
    {
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

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(API.Production);
            _httpClient.DefaultRequestHeaders.Add(RequestHeaders.XToken, token);
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
                var response = await GetAsync(API.Bank.Currency);
                return JsonConvert.DeserializeObject<IEnumerable<CurrencyInfo>>(response.Body);
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
                var response = await GetAsync(API.Personal.ClientInfo);
                return JsonConvert.DeserializeObject<UserInfo>(response.Body);
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
                var response = await PostAsync(API.Personal.Webhook, webhook);
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
                var response = await GetAsync(API.Personal.Statement(account, from, to));
                return JsonConvert.DeserializeObject<IEnumerable<StatementItem>>(response.Body);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        private async Task<(HttpStatusCode Code, string Body)> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return (
                Code: response.StatusCode, 
                Body: await response.Content.ReadAsStringAsync());
            
            // TODO Consider throw an exception on any not 200 status.
        }

        private async Task<(HttpStatusCode Code, string Body)> PostAsync<T>(string url, T value)
        {
            var jsonString = JsonConvert.SerializeObject(value, Formatting.None);
            var content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync(url, content);
            return (
                Code: response.StatusCode, 
                Body: await response.Content.ReadAsStringAsync());
            
            // TODO Consider throw an exception on any not 200 status.
        }

        private static class API
        {
            public const string Production = "https://api.monobank.ua";
            
            public static class Bank
            {
                public const string Currency = "/bank/currency";
            }

            public static class Personal
            {
                public const string ClientInfo = "/personal/client-info";
                public const string Webhook = "/personal/webhook";

                public static string Statement(string account, DateTime from, DateTime to)
                {
                    return "/personal/statement/{account}/{from}/{to}"
                        .Replace("{account}", account)
                        .Replace("{from}", new DateTimeOffset(from).ToUnixTimeSeconds().ToString())
                        .Replace("{to}", new DateTimeOffset(to).ToUnixTimeSeconds().ToString());
                }
            }
        }

        private static class RequestHeaders
        {
            public const string XToken = "X-Token";
        }
    }
}