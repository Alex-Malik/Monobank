using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monobank;

// TODO Add documentation!

/// <summary>
/// Represents data needed to create an invoice. 
/// </summary>
public class Invoice
{
    // Not sure if constructor really needed as this is an input model used to send data.
        
    /// <summary>
    /// Gets the amount of the transaction in the minimum units (coins, cents) of the account currency. 
    /// </summary>
    [JsonProperty("amount")]
    public long Amount { get; set; }

    /// <summary>
    /// Gets or sets the currency code according to ISO 4217. By default it is 980 which is Ukrainian Hryvnia.
    /// </summary>
    [JsonProperty("ccy")]
    public int Ccy { get; set; } = 980;

    /// <summary>
    /// Gets or sets the merchant payment (but actually it's more like invoice) info. E.g. bill number, orders etc.
    /// </summary>
    [JsonProperty("merchantPaymInfo")]
    public MerchantPaymentInfo MerchantPaymentInfo { get; set; }

    /// <summary>
    /// Gets or sets the address which will be used to redirect user after the payment
    /// (both on success and on failure). The GET request will be used.
    /// </summary>
    [JsonProperty("redirectUrl")]
    public string RedirectUrl { get; set; }

    /// <summary>
    /// Gets or sets the webhook address which will be used each time the transaction status changes.
    /// </summary>
    [JsonProperty("webHookUrl")]
    public string WebhookUrl { get; set; }

    /// <summary>
    /// Gets or sets the validity period for the invoice in seconds. If not specified then the invoice will be valid for 24 hours.
    /// </summary>
    [JsonProperty("validity")]
    public long? Validity { get; set; }

    /// <summary>
    /// Gets or sets the type of the operation ("debit" or "hold"). If not specified the "debit" value is used.
    /// For the "hold" operation the hold period is 9 days, otherwise operation canceled.
    /// </summary>
    [JsonProperty("paymentType")]
    public string PaymentType { get; set; }

    /// <summary>
    /// Gets or sets the QR identifier that is used to set a payment amount.
    /// </summary>
    [JsonProperty("qrId")]
    public string QrId { get; set; }

    /// <summary>
    /// Gets or sets the data used to save the card.
    /// The function is disabled by default and requires to contact the Monobank support to activate.
    /// </summary>
    [JsonProperty("saveCardData")]
    public SaveCardData SaveCardData { get; set; }
}
    
/// <summary>
/// Represents the data of the order item used to display the whole order in the app.
/// </summary>
public class BasketOrder
{
    /// <summary>
    /// Gets or sets the name of the good.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the item in the order.
    /// </summary>
    [JsonProperty("qty")]
    public int Qty { get; set; }

    /// <summary>
    /// Gets the total price in the minimum units (coins, cents) of the currency. 
    /// </summary>
    [JsonProperty("sum")]
    public long Sum { get; set; }

    /// <summary>
    /// Gets or sets the link to the icon of the order item.
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; }

    /// <summary>
    /// Gets ot sets the name of the measurement unit for the order item.
    /// </summary>
    [JsonProperty("unit")]
    public string Unit { get; set; }

    /// <summary>
    /// Gets or sets code of the order item. It is required for the fiscalization. 
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the barcode value. Could be required for teh fiscalization.
    /// </summary>
    [JsonProperty("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    /// Gets or sets the text that could be displayed before the name of the order item.
    /// </summary>
    [JsonProperty("header")]
    public string Header { get; set; }

    /// <summary>
    /// Gets or sets the text that could be displayed after the name of the order item.
    /// </summary>
    [JsonProperty("footer")]
    public string Footer { get; set; }

    /// <summary>
    /// Gets or sets the array of the tax codes selected on UKey during the registration.
    /// </summary>
    [JsonProperty("tax")]
    public int[] Tax { get; set; }

    /// <summary>
    /// Gets or sets the code of the "УКТ ЗЕД".
    /// </summary>
    [JsonProperty("uktzed")]
    public string Uktzed { get; set; }
}

public class MerchantPaymentInfo
{
    [JsonProperty("reference")]
    public string Reference { get; set; }

    [JsonProperty("destination")]
    public string Destination { get; set; }

    [JsonProperty("basketOrder")]
    public List<BasketOrder> BasketOrder { get; set; }
}

public class SaveCardData
{
    [JsonProperty("saveCard")]
    public bool SaveCard { get; set; }

    [JsonProperty("walletId")]
    public string WalletId { get; set; }
}