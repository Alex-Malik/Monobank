using Newtonsoft.Json;

namespace Monobank
{
    public class InvoiceInfo
    {
        [JsonProperty("invoiceId")]
        public string InvoiceId { get; set; }
        
        [JsonProperty("pageUrl")]
        public string PageUrl { get; set; }
    }
}