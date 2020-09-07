using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Monobank.Client
{
    public class Monobank
    {
        private const string MonobankBaseUrl = "https://api.monobank.ua/";
        private const string BankCurrencyUrlPart = "/bank/currency";
        private const string UserInfoUrlPart = "/personal/client-info";
        private const string WebhookUrlPart = "/personal/webhook";
        private const string StatementUrlPart = "/personal/statement/{account}/{from}/{to}";
        private readonly string _token;

        public Monobank(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            // TODO Maybe make test request? 

            _token = token;
        }

        public async Task<IEnumerable<CurrencyInfo>> GetCurrencyRatesAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, MonobankBaseUrl + BankCurrencyUrlPart);
                var httpResponse = await httpClient.SendAsync(httpRequest);

                var json = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<CurrencyInfo>>(json);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        public async Task<UserInfo> GetUserInfo()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, MonobankBaseUrl + UserInfoUrlPart);
                httpRequest.Headers.Add("X-Token", _token);
                var httpResponse = await httpClient.SendAsync(httpRequest);

                var json = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<UserInfo>(json);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        public async Task SetWebhookAsync(string newHookUrl)
        {
            if (string.IsNullOrEmpty(newHookUrl))
                throw new ArgumentNullException(newHookUrl);
            var webhook = new Webhook(newHookUrl);

            try
            {
                var httpClient = new HttpClient();
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, MonobankBaseUrl + WebhookUrlPart);
                httpRequest.Headers.Add("X-Token", _token);
                httpRequest.Content = new StringContent(JsonConvert.SerializeObject(webhook));
                await httpClient.SendAsync(httpRequest);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }

        public async Task<IEnumerable<StatementItem>> GetStatementAsync(string account, DateTime from, DateTime to)
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException(nameof(account));
            
            // Is here to see json value outside of try block.
            string json = null;

            try
            {
                var httpClient = new HttpClient();
                var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                    MonobankBaseUrl + StatementUrlPart
                        .Replace("{account}", account)
                        .Replace("{from}", new DateTimeOffset(from).ToUnixTimeSeconds().ToString())
                        .Replace("{to}", new DateTimeOffset(to).ToUnixTimeSeconds().ToString()));
                httpRequest.Headers.Add("X-Token", _token);
                var httpResponse = await httpClient.SendAsync(httpRequest);
                json = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<StatementItem>>(json);
            }
            catch (Exception e)
            {
                throw new MonobankException(e);
            }
        }
    }

    public class MonobankException : Exception
    {
        public MonobankException(Exception e)
        {
        }
    }
}