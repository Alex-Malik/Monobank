using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monobank
{
    // TODO Add documentation!
    
    public class Invoice
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("ccy")]
        public int Ccy { get; set; }

        [JsonProperty("merchantPaymInfo")]
        public MerchantPaymentInfo MerchantPaymentInfo { get; set; }

        [JsonProperty("redirectUrl")]
        public string RedirectUrl { get; set; }

        [JsonProperty("webHookUrl")]
        public string WebHookUrl { get; set; }

        [JsonProperty("validity")]
        public long Validity { get; set; }

        [JsonProperty("paymentType")]
        public string PaymentType { get; set; }

        [JsonProperty("qrId")]
        public string QrId { get; set; }

        [JsonProperty("saveCardData")]
        public SaveCardData SaveCardData { get; set; }
    }
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BasketOrder
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("sum")]
        public long Sum { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("footer")]
        public string Footer { get; set; }

        [JsonProperty("tax")]
        public List<int> Tax { get; set; }

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


}