using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Monobank
{
    using Exceptions;
    
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
        /// <param name="token">used to authenticate a person.</param>
        /// <exception cref="InvalidTokenException">thrown if <paramref name="token"/> is null or empty string.</exception>
        public Monobank(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Api.Production);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(RequestHeaders.XToken, token);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Monobank"/> class with the personal token used
        /// to authenticate a person who makes a request and a custom implementation of the <see cref="HttpClient"/> class.
        /// </summary>
        /// <param name="token">used to authenticate a person.</param>
        /// <param name="httpClient">the implementation of <see cref="HttpClient"/>.</param>
        /// <exception cref="InvalidTokenException">thrown if <paramref name="token"/> is null or empty string.</exception>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="httpClient"/> is null.</exception>
        public Monobank(string token, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Api.Production);
            _httpClient.DefaultRequestHeaders.Clear();
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
            var (code, body) = await GetAsync(Api.Bank.Currency);
            return code switch
            {
                200 => JsonConvert.DeserializeObject<IEnumerable<CurrencyInfo>>(body),
                429 => throw new NotImplementedException("To many requests"),
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
            var (code, body) = await GetAsync(Api.Personal.ClientInfo);
            return code switch
            {
                200 => JsonConvert.DeserializeObject<UserInfo>(body),
                403 => throw new InvalidTokenException(),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Sets the URL which will be used to send POST requests of <see cref="StatementItem"/> objects.
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

            var (code, _) = await PostAsync(Api.Personal.Webhook, webhook);
            if (code != 200)
                throw new NotSupportedException();
        }

        public async Task<Invoice> CreatePaymentRequestAsync(decimal amount, int ccy, string destination, string redirectUrl)
        {
      
            var paymentRequest = new PaymentRequest(amount, ccy, destination, redirectUrl);

            var (code, body) = await PostAsync(Api.Acquiring.MerchantCreate, paymentRequest);
            return code switch
            {
                200 => JsonConvert.DeserializeObject<Invoice>(body),
                403 => throw new InvalidTokenException(),
                _ => throw new NotSupportedException()
            };

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

            var (code, body) = await GetAsync(Api.Personal.Statement(account, @from, to));
            return code switch
            {
                200 => JsonConvert.DeserializeObject<IEnumerable<StatementItem>>(body),
                403 => throw new InvalidTokenException(),
                _ => throw new NotSupportedException()
            };

            bool IsValidInterval() => (to - @from).TotalSeconds > Validation.MaxStatementTimeSpanInSeconds;

            bool IsCallAvailable() => _lastTimeGetStatementCalled.HasValue
                                      && (DateTime.UtcNow - _lastTimeGetStatementCalled.Value).TotalSeconds > Validation.StatementTimeoutBetweenCallsInSeconds;
        }

        #region Acquiring

        public async Task<InvoiceInfo> CreateInvoiceAsync(Invoice invoice)
        {
            // TODO Validate invoice data (at least for required fields)
            
            var (code, body) = await PostAsync(Api.Merchant.Invoice.Create, invoice);
            return code switch
            {
                200 => JsonConvert.DeserializeObject<InvoiceInfo>(body),
                400 => throw new NotImplementedException(),
                403 => throw new InvalidTokenException(),
                404 => throw new NotImplementedException(),
                _ => throw new NotSupportedException()
            };
        }

        #endregion

        /// <summary>
        /// GETs data from the given <see cref="url"/> of Monobank's API.
        /// </summary>
        /// <param name="url">the URL of the request.</param>
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
        /// POSTs data to the given <see cref="url"/> of Monobank's API.
        /// </summary>
        /// <param name="url">the URL path to which the request will be made.</param>
        /// <param name="data">the data which will be POSTed.</param>
        /// <exception cref="MonobankRequestException">Thrown if request to Monobank's API failed.</exception>
        /// <returns>Returns the tuple of HTTP Status Code and JSON Body received in response from Monobank's API.</returns>
        private async Task<(int Code, string Body)> PostAsync<T>(string url, T data)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(data, Formatting.None);
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
        private static class Api
        {
            public const string Production = "https://api.monobank.ua";

            public static class Bank
            {
                public const string Currency = "/bank/currency";
            }
            public static class Acquiring
            {
                public const string MerchantCreate = "/api/merchant/invoice/create";
            }
            public static class Personal
            {
                public const string ClientInfo = "/personal/client-info";
                public const string Webhook = "/personal/webhook";

                public static string Statement(string account, DateTime from, DateTime to) 
                    => "/personal/statement/{account}/{from}/{to}"
                        .Replace("{account}", account)
                        .Replace("{from}", new DateTimeOffset(from).ToUnixTimeSeconds().ToString())
                        .Replace("{to}", new DateTimeOffset(to).ToUnixTimeSeconds().ToString());
            }

            public static class Merchant
            {
                public static class Invoice
                {
                    public const string Create = "/api/merchant/invoice/create";

                    public static string Status(string invoiceId) 
                        => $"/api/merchant/invoice/status?invoiceId={invoiceId}";
                }
            }
            
        }

        /// <summary>
        /// Encapsulates constants related to HTTP Headers used when making calls to Monobank's API.
        /// </summary>
        private static class RequestHeaders
        {
            public const string XToken = "uyV-54sYFBWRUmgHWdNBW6qmz8V35Nzy1rY-Hc3EQ5sY";
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