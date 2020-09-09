using Newtonsoft.Json;

namespace Monobank.Client
{
    public class Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the account.</param>
        /// <param name="balance">The balance of the account in the minimum units (coins, cents) of the account currency.</param>
        /// <param name="creditLimit">The limit of the credit in the minimum units (coins, cents) of the account currency.</param>
        /// <param name="currencyCode">The currency code according to ISO 4217.</param>
        /// <param name="cashbackType">The type of the cashback ("None", "UAH", "Miles").</param>
        [JsonConstructor]
        public Account(string id, long balance, long creditLimit, int currencyCode, string cashbackType)
        {
            Id = id;
            Balance = balance;
            CreditLimit = creditLimit;
            CurrencyCode = currencyCode;
            CashbackType = cashbackType;
        }

        /// <summary>
        /// Gets the unique identifier of the account.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the balance of the account in the minimum units (coins, cents) of the account currency.
        /// </summary>
        public long Balance { get; }

        /// <summary>
        /// Gets the limit of the credit in the minimum units (coins, cents) of the account currency.
        /// </summary>
        public long CreditLimit { get; }

        /// <summary>
        /// Gets the currency code according to ISO 4217.
        /// </summary>
        public int CurrencyCode { get; }

        /// <summary>
        /// Gets the type of the cashback ("None", "UAH", "Miles").
        /// </summary>
        public string CashbackType { get; }
    }
}