using System.Collections.Generic;

namespace Worldpay
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

        public override bool Equals(object obj)
        {
            var url = obj as RedirectUrl;
            return url != null &&
                   Url == url.Url;
        }

        public override int GetHashCode()
        {
            return -1915121810 + EqualityComparer<string>.Default.GetHashCode(Url);
        }

    }
}
