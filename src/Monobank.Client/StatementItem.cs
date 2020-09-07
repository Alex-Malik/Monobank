using System;
using Newtonsoft.Json;

namespace Monobank.Client
{
    public class StatementItem
    {
        [JsonConstructor]
        internal StatementItem(string id, long time, string description, int mcc, bool hold, long amount,
            long operationAmount, int currencyCode, long commissionRate, long cashbackAmount, long balance)
        {
            Id = id;
            Time = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
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

        public string Id { get; }
        public DateTime Time { get; }
        public string Description { get; }
        public int Mcc { get; }
        public bool Hold { get; }
        public long Amount { get; }
        public long OperationAmount { get; }
        public int CurrencyCode { get; }
        public long CommissionRate { get; }
        public long CashbackAmount { get; }
        public long Balance { get; }
    }
}