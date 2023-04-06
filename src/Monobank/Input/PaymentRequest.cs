namespace Monobank
{
    /// <summary>
    /// Represents data for initiating a new payment request.
    /// </summary>
    internal class PaymentRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRequest"/> class with the given payment information.
        /// </summary>
        /// <param name="amount">The payment amount.</param>
        /// <param name="ccy">The payment currency code.</param>
        /// <param name="destination">The payment destination.</param>
        /// <param name="redirectUrl">The redirect URL after payment completion.</param>
       
        internal PaymentRequest(decimal amount, int ccy, string destination, string redirectUrl)
        {
            Amount = amount;
            Ccy = ccy;
            RedirectUrl = redirectUrl;
            MerchantPaymInfo = new MerchantPaymentInfo
            {         
                Destination = destination,   
            };
        }
        /// <summary>
        /// Gets the payment amount.
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// Gets the payment currency code.
        /// </summary>
        public int Ccy { get; }

        /// <summary>
        /// Gets the merchant payment information.
        /// </summary>
        ///  /// <summary>
        /// Gets or sets the redirect URL after payment completion.
        /// </summary>
        public string RedirectUrl { get; set; }
        public MerchantPaymentInfo MerchantPaymInfo { get; }
    }

    /// <summary>
    /// Represents merchant payment information for a payment request.
    /// </summary>
    internal class MerchantPaymentInfo
    {
        /// <summary>
        /// Gets or sets the payment destination.
        /// </summary>
        public string Destination { get; set; }

       
    }
}