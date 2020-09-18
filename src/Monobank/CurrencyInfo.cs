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
        /// <param name="currencyCodeA">The A's currency code according to ISO 4217.</param>
        /// <param name="currencyCodeB">The B's currency code according to ISO 4217.</param>
        /// <param name="date">The time of the currency rate in seconds in Unix format.</param>
        /// <param name="rateSell">The sell rate for this currency pair.</param>
        /// <param name="rateBuy">The buy rate for this currency pair.</param>
        /// <param name="rateCross">The cross rate for this currency pair.</param>
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
        public int CurrencyCodeA { get; }

        /// <summary>
        /// Gets the B's currency code according to ISO 4217.
        /// </summary>
        public int CurrencyCodeB { get; }

        /// <summary>
        /// Gets the time of the currency rate in seconds in Unix format.
        /// </summary>
        public long Date { get; }

        /// <summary>
        /// Gets the sell rate for this currency pair.
        /// </summary>
        public float RateSell { get; }

        /// <summary>
        /// Gets the buy rate for this currency pair.
        /// </summary>
        public float RateBuy { get; }

        /// <summary>
        /// Gets the cross rate for this currency pair.
        /// </summary>
        public float RateCross { get; }
    }
}