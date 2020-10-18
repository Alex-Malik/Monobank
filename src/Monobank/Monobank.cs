using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Monobank.Exceptions;
using Newtonsoft.Json;

namespace Monobank
{
    /// <summary>
    /// Provides methods to communicate with Monobank's API.
    /// </summary>
    public class Monobank
    {
        private readonly HttpClient _httpClient;
        private readonly DateTime? _lastTimeGetStatementCalled = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Monobank"/> class with the personal token used
        /// to authenticate a person who makes a request.
        /// </summary>
        /// <param name="token">The token used to authenticate a person.</param>
        /// <exception cref="InvalidTokenException">Thrown if <paramref name="token"/> is null or empty string.</exception>
        public Monobank(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new InvalidTokenException();

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(API.Production);
            _httpClient.DefaultRequestHeaders.Add(RequestHeaders.XToken, token);
        }

        /// <summary>
        /// Gets a basic list of Monobank exchange rates. The server caches and updates
        /// information no more than once every 5 minutes.
        /// </summary>
        /// <exception cref="MonobankRequestException">Thrown if request to Monobank's API failed.</exception>
        /// <returns>The list of Monobank exchange rates represented by <see cref="CurrencyInfo"/> objects.</returns>
        public async Task<IEnumerable<CurrencyInfo>> GetCurrencyRatesAsync()
        {
            var (code, body) = await GetAsync(API.Bank.Currency);
            return code switch
            {
                200 => JsonConvert.DeserializeObject<IEnumerable<CurrencyInfo>>(body),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Gets a personal client's information and accounts. This function can be used only ones each 60 seconds.
        /// </summary>
        /// <exception cref="MonobankRequestException">Thrown if request to Monobank's API failed.</exception>
        /// <returns>The <see cref="UserInfo"/> object containing client's personal information and accounts.</returns>
        public async Task<UserInfo> GetUserInfoAsync()
        {
            var (code, body) = await GetAsync(API.Personal.ClientInfo);
            return code switch
            {
                200 => JsonConvert.DeserializeObject<UserInfo>(body),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Sets the URL which will be used to send POST requests of <see cref=""/> objects.
        /// <para/> 
        /// <para/>
        /// If the user service under the URL will not respond within 5 seconds the request will be send again in 60
        /// and 600 seconds. If no response is received on the third attempt, the function will be disabled.
        /// </summary>
        /// <param name="newHookUrl">The new webhook URL to set up.</param>
        /// <exception cref="InvalidWebhookUrlException">Thrown if provided <see cref="newHookUrl"/> argument is null or empty string.</exception>
        /// <exception cref="MonobankRequestException">Thrown if request to Monobank's API failed.</exception>
        public async Task SetWebhookAsync(string newHookUrl)
        {
            if (string.IsNullOrEmpty(newHookUrl) /* TODO Consider validate URL. */)
                throw new InvalidWebhookUrlException();

            var webhook = new Webhook(newHookUrl);

            var (code, _) = await PostAsync(API.Personal.Webhook, webhook);
            if (code != 200)
                throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a statement for specified account within given period of rime.
        /// </summary>
        /// <param name="account">The unique identifier of the account. Specify "0" to get default account.</param>
        /// <param name="from">The UTC date and time to get statement from.</param>
        /// <param name="to">The UTC date and time until the statement should be loaded.</param>
        /// <exception cref="InvalidAccountException">Thrown if account number is null or empty string.</exception>
        /// <exception cref="InvalidStatementPeriodException">Thrown if period between <see cref="from"/> and <see cref="to"/> is longer then 31 day and 1 hour.</exception>
        /// <exception cref="ToFrequentCallException">Thrown if previous call of this method has been made less then minute ago.</exception>
        /// <exception cref="MonobankRequestException">Thrown if request to Monobank's API failed.</exception>
        /// <returns>The list of <see cref="StatementItem"/>s.</returns>
        public async Task<IEnumerable<StatementItem>> GetStatementAsync(string account, DateTime from, DateTime to)
        {
            if (string.IsNullOrEmpty(account))
                throw new InvalidAccountException();
            if (IsValidInterval())
                throw new InvalidStatementPeriodException();
            if (IsCallAvailable())
                throw new ToFrequentCallException();

            var (code, body) = await GetAsync(API.Personal.Statement(account, @from, to));
            return code switch
            {
                200 => JsonConvert.DeserializeObject<IEnumerable<StatementItem>>(body),
                _ => throw new NotSupportedException()
            };

            bool IsValidInterval() => (to - @from).TotalSeconds > Validation.MaxStatementTimeSpanInSeconds;

            bool IsCallAvailable() => _lastTimeGetStatementCalled.HasValue
                                      && (DateTime.UtcNow - _lastTimeGetStatementCalled.Value).TotalSeconds > Validation.StatementTimeoutBetweenCallsInSeconds;
        }

        /// <summary>
        /// Sends GET request to the Monobank's API with given <see cref="url"/> or url path.
        /// </summary>
        /// <param name="url">The URL or URL path to which make a request.</param>
        /// <exception cref="MonobankRequestException">Thrown if request to Monobank's API failed.</exception>
        /// <returns>Returns the tuple of HTTP Status Code and JSON Body received in response from Monobank's API.</returns>
        private async Task<(int Code, string Body)> GetAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                return (
                    Code: (int) response.StatusCode,
                    Body: await response.Content.ReadAsStringAsync());

                // TODO Consider throw an exception on any not 200 status.
            }
            catch (HttpRequestException exception)
            {
                throw new MonobankRequestException(exception);
            }
        }

        /// <summary>
        /// Sends POST request to the Monobank's API with given <see cref="url"/> or url path.
        /// </summary>
        /// <param name="url">The URL or URL path to which make a request.</param>
        /// <exception cref="MonobankRequestException">Thrown if request to Monobank's API failed.</exception>
        /// <returns>Returns the tuple of HTTP Status Code and JSON Body received in response from Monobank's API.</returns>
        private async Task<(int Code, string Body)> PostAsync<T>(string url, T value)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(value, Formatting.None);
                var content = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = await _httpClient.PostAsync(url, content);
                return (
                    Code: (int) response.StatusCode,
                    Body: await response.Content.ReadAsStringAsync());

                // TODO Consider throw an exception on any not 200 status.
            }
            catch (HttpRequestException exception)
            {
                throw new MonobankRequestException(exception);
            }
        }

        /// <summary>
        /// Encapsulates constants related to API requests, URLs and URL parts.
        /// </summary>
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

        /// <summary>
        /// Encapsulates constants related to HTTP Headers used when making calls to Monobank's API.
        /// </summary>
        private static class RequestHeaders
        {
            public const string XToken = "X-Token";
        }

        /// <summary>
        /// Encapsulates constants used by validation methods and checks.
        /// </summary>
        private static class Validation
        {
            public const int MaxStatementTimeSpanInSeconds = 2682000;
            public const int StatementTimeoutBetweenCallsInSeconds = 60;
        }
    }
}