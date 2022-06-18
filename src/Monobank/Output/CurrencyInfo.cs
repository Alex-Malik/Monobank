using Newtonsoft.Json;

namespace Monobank
{
    /// <summary>
    /// Represents a currency pair with their sell/buy/cross rates.
    /// </summary>
    public class CurrencyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyInfo"/> class.
        /// </summary>
        /// <param name="currencyCodeA">the A's currency code according to ISO 4217 (e.g. 840 is USD).</param>
        /// <param name="currencyCodeB">the B's currency code according to ISO 4217 (e.g. 980 is UAH).</param>
        /// <param name="date">the time of the currency rate in seconds in Unix format.</param>
        /// <param name="rateSell">the sell rate for this currency pair (meaning how the bank sales A for B).</param>
        /// <param name="rateBuy">the buy rate for this currency pair (meaning how the bank buys A for B).</param>
        /// <param name="rateCross">the cross rate for this currency pair.</param>
        [JsonConstructor]
        internal CurrencyInfo(int currencyCodeA, int currencyCodeB, long date, float rateSell, float rateBuy, float rateCross)
        {
            CurrencyCodeA = currencyCodeA;
            CurrencyCodeB = currencyCodeB;
            Date = date;
            RateSell = rateSell;
            RateBuy = rateBuy;
            RateCross = rateCross;
        }

        /// <summary>
        /// Gets the A's currency code according to ISO 4217.
        /// </summary>
        [JsonProperty("currencyCodeA")]
        public int CurrencyCodeA { get; }

        /// <summary>
        /// Gets the B's currency code according to ISO 4217.
        /// </summary>
        [JsonProperty("currencyCodeB")]
        public int CurrencyCodeB { get; }

        /// <summary>
        /// Gets the time of the currency rate in seconds in Unix format.
        /// </summary>
        [JsonProperty("date")]
        public long Date { get; }

        /// <summary>
        /// Gets the sell rate for this currency pair.
        /// </summary>
        [JsonProperty("rateSell")]
        public float RateSell { get; }

        /// <summary>
        /// Gets the buy rate for this currency pair.
        /// </summary>
        [JsonProperty("rateBuy")]
        public float RateBuy { get; }

        /// <summary>
        /// Gets the cross rate for this currency pair.
        /// </summary>
        [JsonProperty("rateCross")]
        public float RateCross { get; }
    }
}