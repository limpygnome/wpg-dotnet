using System;
using System.Text;
using System.Net;
using wpg.domain.payment;
using wpg.@internal.xml;

namespace wpg.builder
{
    public class PaymentPagesRedirectBuilder
    {
        public PaymentPagesRedirectBuilder(string orderUrl)
        {
            this.OrderUrl = orderUrl;
        }

        public string OrderUrl { get; set; }

        public string SuccessUrl { get; set; }
        public string PendingUrl { get; set; }
        public string FailureUrl { get; set; }
        public string ErrorUrl { get; set; }
        public string CancelUrl { get; set; }
        public string PreferredPaymentMethod { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }

        public PaymentMethodType? PreferredPaymentMethodType
        {
            get
            {
                return PaymentMethodTypeTranslator.getType(PreferredPaymentMethod);
            }
            set
            {
                PreferredPaymentMethod = PaymentMethodTypeTranslator.getMask(value);
            }
        }

        public string Build()
        {
            StringBuilder builder = new StringBuilder(OrderUrl);

            if (String.IsNullOrWhiteSpace(OrderUrl))
            {
                throw new ArgumentException("Invalid order URL");
            }

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

            string url = builder.ToString();
            return url;
        }
        
        private string encode(string url)
        {
            return WebUtility.UrlEncode(url);
        }

    }
}
