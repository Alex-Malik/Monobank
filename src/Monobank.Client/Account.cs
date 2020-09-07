using Newtonsoft.Json;

namespace Monobank.Client
{
    public class Account
    {
        [JsonConstructor]
        public Account(string id, long balance, long creditLimit, int currencyCode, string cashbackType)
        {
            Id = id;
            Balance = balance;
            CreditLimit = creditLimit;
            CurrencyCode = currencyCode;
            CashbackType = cashbackType;
        }

        public string Id { get; }

        public long Balance { get; }

        public long CreditLimit { get; }

        public int CurrencyCode { get; }

        public string CashbackType { get; }
    }
}