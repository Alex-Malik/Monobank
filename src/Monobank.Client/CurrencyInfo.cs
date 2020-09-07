using Newtonsoft.Json;

namespace Monobank.Client
{
    public class CurrencyInfo
    {
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

        public int CurrencyCodeA { get; }

        public int CurrencyCodeB { get; }

        public long Date { get; }

        public float RateSell { get; }

        public float RateBuy { get; }

        public float RateCross { get; }
    }
}