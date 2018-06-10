using System;
using wpg.builder;

namespace wpg.domain.redirect
{
    public class RedirectUrl
    {
        public RedirectUrl(string url)
        {
            this.Url = url;
        }

        public PaymentPagesRedirectBuilder CreatePaymentPagesBuilder()
        {
            return new PaymentPagesRedirectBuilder(Url);
        }

        public string Url { get; private set; }

    }
}
