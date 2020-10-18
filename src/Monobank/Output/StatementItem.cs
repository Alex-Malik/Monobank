using Newtonsoft.Json;

namespace Monobank
{
    /// <summary>
    /// Represents the transaction belonging to the account for which the statement is requested.
    /// </summary>
    public class StatementItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatementItem"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <param name="time">The time of the transaction in seconds in Unix format.</param>
        /// <param name="description">The description of the transaction.</param>
        /// <param name="mcc">The Merchant Category Code according to ISO 18245.</param>
        /// <param name="hold">The value indicating whether transaction authorization hold.</param>
        /// <param name="amount">The amount of the transaction in the minimum units (coins, cents) of the account currency.</param>
        /// <param name="operationAmount">The amount of the transaction in the minimum units (coins, cents) of the transaction currency.</param>
        /// <param name="currencyCode">The currency code according to ISO 4217.</param>
        /// <param name="commissionRate">The commission rate in the minimum units (coins, cents) of the currency.</param>
        /// <param name="cashbackAmount">The cashback amount in the minimum units (coins, cents) of the currency.</param>
        /// <param name="balance">The balance of the account in the minimum units (coins, cents) of the currency.</param>
        [JsonConstructor]
        internal StatementItem(string id, long time, string description, int mcc, bool hold, long amount,
            long operationAmount, int currencyCode, long commissionRate, long cashbackAmount, long balance)
        {
            Id = id;
            Time = time; // DateTimeOffset.FromUnixTimeSeconds(time).DateTime.ToLocalTime();
            Description = description;
            Mcc = mcc;
            Hold = hold;
            Amount = amount;
            OperationAmount = operationAmount;
            CurrencyCode = currencyCode;
            CommissionRate = commissionRate;
            CashbackAmount = cashbackAmount;
            Balance = balance;
        }

        /// <summary>
        /// Gets the unique identifier of the transaction.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the time of the transaction in seconds in Unix format.
        /// </summary>
        public long Time { get; }

        /// <summary>
        /// Gets the description of the transaction.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the Merchant Category Code according to ISO 18245.
        /// </summary>
        public int Mcc { get; }

        /// <summary>
        /// Gets the value indicating whether transaction authorization hold.
        /// </summary>
        public bool Hold { get; }

        /// <summary>
        /// Gets the amount of the transaction in the minimum units (coins, cents) of the account currency.
        /// </summary>
        public long Amount { get; }

        /// <summary>
        /// Gets the amount of the transaction in the minimum units (coins, cents) of the transaction currency.
        /// </summary>
        public long OperationAmount { get; }

        /// <summary>
        /// Gets the currency code according to ISO 4217.
        /// </summary>
        public int CurrencyCode { get; }

        /// <summary>
        /// Gets the commission rate in the minimum units (coins, cents) of the currency.
        /// </summary>
        public long CommissionRate { get; }

        /// <summary>
        /// Gets the cashback amount in the minimum units (coins, cents) of the currency.
        /// </summary>
        public long CashbackAmount { get; }

        /// <summary>
        /// Gets the balance of the account in the minimum units (coins, cents) of the currency.
        /// </summary>
        public long Balance { get; }
    }
}