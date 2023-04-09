using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monobank;

/// <summary>
/// Represents the detailed information about the invoice status (more like about invoice state actually).
/// </summary>
public class InvoiceStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceStatus"/> class.
    /// </summary>
    /// <param name="invoiceId">the identifier of the invoice;</param>
    /// <param name="status">the status of the invoice;</param>
    /// <param name="failureReason">the reason why invoice failed;</param>
    /// <param name="amount"></param>
    /// <param name="ccy"></param>
    /// <param name="finalAmount"></param>
    /// <param name="createdDate"></param>
    /// <param name="modifiedDate"></param>
    /// <param name="reference"></param>
    /// <param name="cancels"></param>
    /// <param name="splits"></param>
    /// <param name="wallet"></param>
    [JsonConstructor]
    public InvoiceStatus(string invoiceId, string status, string failureReason, long amount, int ccy, long finalAmount,
        DateTime createdDate, DateTime modifiedDate, string reference, IEnumerable<CancelItem> cancels,
        IEnumerable<SplitItem> splits, Wallet wallet)
    {
        InvoiceId = invoiceId;
        Status = status;
        FailureReason = failureReason;
        Amount = amount;
        Ccy = ccy;
        FinalAmount = finalAmount;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        Reference = reference;
        Cancels = cancels;
        Splits = splits;
        Wallet = wallet;
    }

    [JsonProperty("invoiceId")] public string InvoiceId { get; }

    /// <summary>
    /// Gets the status of the invoice: "created" "processing" "hold" "success" "failure" "reversed" "expired".
    /// </summary>
    [JsonProperty("status")] public string Status { get; }

    [JsonProperty("failureReason")] public string FailureReason { get; }

    [JsonProperty("amount")] public long Amount { get; }

    [JsonProperty("ccy")] public int Ccy { get; }

    [JsonProperty("finalAmount")] public long FinalAmount { get; }

    [JsonProperty("createdDate")] public DateTime CreatedDate { get; }

    [JsonProperty("modifiedDate")] public DateTime ModifiedDate { get; }

    [JsonProperty("reference")] public string Reference { get; }

    [JsonProperty("cancelList")] public IEnumerable<CancelItem> Cancels { get; }

    [JsonProperty("splitList")] public IEnumerable<SplitItem> Splits { get; }

    [JsonProperty("walletData")] public Wallet Wallet { get; }
}

public class CancelItem
{
    public CancelItem(string status, int amount, int ccy, DateTime createdDate, DateTime modifiedDate,
        string approvalCode, string rrn, string extRef)
    {
        Status = status;
        Amount = amount;
        Ccy = ccy;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        ApprovalCode = approvalCode;
        Rrn = rrn;
        ExtRef = extRef;
    }

    [JsonProperty("status")] public string Status { get; }

    [JsonProperty("amount")] public int Amount { get; }

    [JsonProperty("ccy")] public int Ccy { get; }

    [JsonProperty("createdDate")] public DateTime CreatedDate { get; }

    [JsonProperty("modifiedDate")] public DateTime ModifiedDate { get; }

    [JsonProperty("approvalCode")] public string ApprovalCode { get; }

    [JsonProperty("rrn")] public string Rrn { get; }

    [JsonProperty("extRef")] public string ExtRef { get; }
}

public class SplitItem
{
    [JsonConstructor]
    public SplitItem(string tin, string name, string iban, int amount, int ccy, string status)
    {
        Tin = tin;
        Name = name;
        Iban = iban;
        Amount = amount;
        Ccy = ccy;
        Status = status;
    }

    [JsonProperty("tin")] public string Tin { get; }

    [JsonProperty("name")] public string Name { get; }

    [JsonProperty("iban")] public string Iban { get; }

    [JsonProperty("amount")] public int Amount { get; }

    [JsonProperty("ccy")] public int Ccy { get; }

    [JsonProperty("status")] public string Status { get; }
}

/// <summary>
/// Represents the information about buyer's card.
/// </summary>
public class Wallet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Wallet"/> class.
    /// </summary>
    /// <param name="cardToken">the card token.</param>
    /// <param name="walletId">the identifier of the buyer's wallet.</param>
    /// <param name="status">the status of the card tokenization.</param>
    [JsonConstructor]
    public Wallet(string cardToken, string walletId, string status)
    {
        CardToken = cardToken;
        WalletId = walletId;
        Status = status;
    }

    /// <summary>
    /// Gets the card token.
    /// </summary>
    [JsonProperty("cardToken"), JsonRequired]
    public string CardToken { get; }

    /// <summary>
    /// Gets the identifier of the buyer's wallet.
    /// </summary>
    [JsonProperty("walletId"), JsonRequired]
    public string WalletId { get; }

    /// <summary>
    /// Gets the status of the card tokenization: "new", "created", "failure".
    /// </summary>
    [JsonProperty("status"), JsonRequired]
    public string Status { get; }
}