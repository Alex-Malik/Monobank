using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        /// <param name="id">the unique identifier of the transaction.</param>
        /// <param name="time">the time of the transaction in seconds in Unix format.</param>
        /// <param name="description">the description of the transaction.</param>
        /// <param name="mcc">the Merchant Category Code according to ISO 18245.</param>
        /// <param name="originalMcc">the original Merchant Category Code according to ISO 18245.</param>
        /// <param name="hold">the value indicating whether transaction authorization hold.</param>
        /// <param name="amount">the amount of the transaction in the minimum units (coins, cents) of the account currency.</param>
        /// <param name="operationAmount">the amount of the transaction in the minimum units (coins, cents) of the transaction currency.</param>
        /// <param name="currencyCode">the currency code according to ISO 4217.</param>
        /// <param name="commissionRate">the commission rate in the minimum units (coins, cents) of the currency.</param>
        /// <param name="cashbackAmount">the cashback amount in the minimum units (coins, cents) of the currency.</param>
        /// <param name="balance">the balance of the account in the minimum units (coins, cents) of the currency.</param>
        /// <param name="comment">the transaction comment if set by the client.</param>
        /// <param name="receiptId">the unique identifier of check.gov.ua.</param>
        /// <param name="invoiceId">the unique identifier of the invoice in case if payment received by the invoice.</param>
        /// <param name="counterEdrpou">the "ЄДРПОУ" code of the counter-agent which appears only for PE ("ФОП") accounts.</param>
        /// <param name="counterIban">the IBAN of the counter-agent which appears only for PE ("ФОП") accounts.</param>
        [JsonConstructor]
        internal StatementItem(string id, long time, string description, int mcc, int originalMcc, bool hold, 
            long amount, long operationAmount, int currencyCode, long commissionRate, long cashbackAmount, long balance,
            string comment, string receiptId, string invoiceId, string counterEdrpou, string counterIban)
        {
            Id = id;
            Time = time; // DateTimeOffset.FromUnixTimeSeconds(time).DateTime.ToLocalTime();
            Description = description;
            Mcc = mcc;
            OriginalMcc = originalMcc;
            Hold = hold;
            Amount = amount;
            OperationAmount = operationAmount;
            CurrencyCode = currencyCode;
            CommissionRate = commissionRate;
            CashbackAmount = cashbackAmount;
            Balance = balance;
            Comment = comment;
            ReceiptId = receiptId;
            InvoiceId = invoiceId;
            CounterEdrpou = counterEdrpou;
            CounterIban = counterIban;
        }

        /// <summary>
        /// Gets the unique identifier of the transaction.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; }

        /// <summary>
        /// Gets the time of the transaction in seconds in Unix format.
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; }

        /// <summary>
        /// Gets the description of the transaction.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; }

        /// <summary>
        /// Gets the Merchant Category Code according to ISO 18245.
        /// </summary>
        [JsonProperty("mcc")]
        public int Mcc { get; }

        /// <summary>
        /// Gets the original Merchant Category Code according to ISO 18245.
        /// </summary>
        [JsonProperty("originalMcc")]
        public int OriginalMcc { get; }
        
        /// <summary>
        /// Gets the value indicating whether transaction authorization hold.
        /// </summary>
        [JsonProperty("hold")]
        public bool Hold { get; }

        /// <summary>
        /// Gets the amount of the transaction in the minimum units (coins, cents) of the account currency.
        /// </summary>
        [JsonProperty("amount")]
        public long Amount { get; }

        /// <summary>
        /// Gets the amount of the transaction in the minimum units (coins, cents) of the transaction currency.
        /// </summary>
        [JsonProperty("operationAmount")]
        public long OperationAmount { get; }

        /// <summary>
        /// Gets the currency code according to ISO 4217.
        /// </summary>
        [JsonProperty("currencyCode")]
        public int CurrencyCode { get; }

        /// <summary>
        /// Gets the commission rate in the minimum units (coins, cents) of the currency.
        /// </summary>
        [JsonProperty("commissionRate")]
        public long CommissionRate { get; }

        /// <summary>
        /// Gets the cashback amount in the minimum units (coins, cents) of the currency.
        /// </summary>
        [JsonProperty("cashbackAmount")]
        public long CashbackAmount { get; }

        /// <summary>
        /// Gets the balance of the account in the minimum units (coins, cents) of the currency.
        /// </summary>
        [JsonProperty("balance")]
        public long Balance { get; }

        /// <summary>
        /// Gets the transaction comment if set by the client.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; }

        /// <summary>
        /// Gets the unique identifier of check.gov.ua.
        /// </summary>
        [JsonProperty("receiptId")]
        public string ReceiptId { get; }

        /// <summary>
        /// Gets the unique identifier of the invoice in case if payment received by the invoice.
        /// </summary>
        [JsonProperty("invoiceId")]
        public string InvoiceId { get; }

        /// <summary>
        /// Gets the "ЄДРПОУ" code of the counter-agent which appears only for individual entrepreneur accounts. 
        /// </summary>
        [JsonProperty("counterEdrpou")]
        public string CounterEdrpou { get; }

        /// <summary>
        /// Gets the IBAN of the counter-agent which appears only for individual entrepreneur accounts. 
        /// </summary>
        [JsonProperty("counterIban")]
        public string CounterIban { get; }
    }
}