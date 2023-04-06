using Newtonsoft.Json;

namespace Monobank
{
    /// <summary>
    /// Represents an invoice information.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Invoice"/> class.
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice.</param>
        /// <param name="pageUrl">The page URL to pay the invoice.</param>
        [JsonConstructor]
        public Invoice(string invoiceId, string pageUrl)
        {
            InvoiceId = invoiceId;
            PageUrl = pageUrl;
        }

        /// <summary>
        /// Gets the unique identifier of the invoice.
        /// </summary>
        [JsonProperty("invoiceId")]
        public string InvoiceId { get; }

        /// <summary>
        /// Gets the page URL to pay the invoice.
        /// </summary>
        [JsonProperty("pageUrl")]
        public string PageUrl { get; }
    }
}
