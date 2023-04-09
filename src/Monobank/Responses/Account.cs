using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monobank;

/// <summary>
/// Represents a user's account information.
/// </summary>
public class Account
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class.
    /// </summary>
    /// <param name="id">the unique identifier of the account.</param>
    /// <param name="sendId">the unique identifier of the https://send.monobank.ua/{sendId} service.</param>
    /// <param name="balance">the balance of the account in the minimum units (coins, cents) of the account currency.</param>
    /// <param name="creditLimit">the limit of the credit in the minimum units (coins, cents) of the account currency.</param>
    /// <param name="type">the type of the account.</param>
    /// <param name="currencyCode">the currency code according to ISO 4217.</param>
    /// <param name="cashbackType">the type of the cashback ("None", "UAH", "Miles").</param>
    /// <param name="maskedPan">the enumeration of masked card numbers (for premium cards their could be more than one).</param>
    /// <param name="iban">the international bank account number.</param>
    [JsonConstructor]
    internal Account(string id, string sendId, long balance, long creditLimit, string type, int currencyCode, string cashbackType, IEnumerable<string> maskedPan, string iban)
    {
        Id = id;
        SendId = sendId;
        Balance = balance;
        CreditLimit = creditLimit;
        Type = type;
        CurrencyCode = currencyCode;
        CashbackType = cashbackType;
        MaskedPan = maskedPan;
        Iban = iban;
    }

    /// <summary>
    /// Gets the unique identifier of the account.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; }

    /// <summary>
    /// Gets the unique identifier of the https://send.monobank.ua/{sendId} service.
    /// </summary>
    [JsonProperty("sendId")]
    public string SendId { get; }
        
    /// <summary>
    /// Gets the balance of the account in the minimum units (coins, cents) of the account currency.
    /// </summary>
    [JsonProperty("balance")]
    public long Balance { get; }

    /// <summary>
    /// Gets the limit of the credit in the minimum units (coins, cents) of the account currency.
    /// </summary>
    [JsonProperty("creditLimit")]
    public long CreditLimit { get; }

    /// <summary>
    /// Gets the type of the account.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; }

    /// <summary>
    /// Gets the currency code according to ISO 4217.
    /// </summary>
    [JsonProperty("currencyCode")]
    public int CurrencyCode { get; }
        
    /// <summary>
    /// Gets the type of the cashback ("None", "UAH", "Miles").
    /// </summary>
    [JsonProperty("cashbackType")]
    public string CashbackType { get; }

    /// <summary>
    /// Gets the enumeration of masked card numbers (for premium cards their could be more than one).
    /// </summary>
    [JsonProperty("maskedPan")]
    public IEnumerable<string> MaskedPan { get; }

    /// <summary>
    /// Gets the international bank account number.
    /// </summary>
    [JsonProperty("iban")]
    public string Iban { get; }
}