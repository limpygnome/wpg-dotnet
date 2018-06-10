using System;
using System.Text;
using System.Net;

namespace wpg.builder
{
    public class PaymentPagesRedirectBuilder
    {
        public PaymentPagesRedirectBuilder(string orderUrl)
        {
            this.OrderUrl = orderUrl;
        }

        private string OrderUrl { get; set; }

        private string SuccessUrl { get; set; }
        private string PendingUrl { get; set; }
        private string FailureUrl { get; set; }
        private string ErrorUrl { get; set; }
        private string CancelUrl { get; set; }
        private string PreferredPaymentMethod { get; set; }
        private string Country { get; set; }
        private string Language { get; set; }

        public String build()
        {
            StringBuilder builder = new StringBuilder(OrderUrl);

            if (!String.IsNullOrWhiteSpace(SuccessUrl))
            {
                builder.Append("&successURL=").Append(encode(SuccessUrl));
            }
            if (!String.IsNullOrWhiteSpace(PendingUrl))
            {
                builder.Append("&pendingURL=").Append(encode(PendingUrl));
            }
            if (!String.IsNullOrWhiteSpace(FailureUrl))
            {
                builder.Append("&failureURL=").Append(encode(FailureUrl));
            }
            if (!String.IsNullOrWhiteSpace(ErrorUrl))
            {
                builder.Append("&errorURL=").Append(encode(ErrorUrl));
            }
            if (!String.IsNullOrWhiteSpace(CancelUrl))
            {
                builder.Append("&cancelURL=").Append(encode(CancelUrl));
            }
            if (!String.IsNullOrWhiteSpace(PreferredPaymentMethod))
            {
                builder.Append("&preferredPaymentMethod=").Append(encode(PreferredPaymentMethod.ToLower()));
            }
            if (!String.IsNullOrWhiteSpace(Country))
            {
                builder.Append("&country=").Append(encode(Country.ToLower()));
            }
            if (!String.IsNullOrWhiteSpace(Language))
            {
                builder.Append("&language=").Append(encode(Language.ToLower()));
            }

            String url = builder.ToString();
            return url;
        }
        
        private string encode(string url)
        {
            return WebUtility.UrlEncode(url);
        }

    }
}
