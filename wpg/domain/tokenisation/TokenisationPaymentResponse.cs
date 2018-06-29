using wpg.domain.payment;
using wpg.domain.redirect;

namespace wpg.domain.tokenisation
{
    public class TokenisationPaymentResponse
    {
        public PaymentResponse PaymentResponse { get; private set; }
        public RedirectUrl CaptureCvcUrl { get; private set; }

        public TokenisationPaymentResponse(PaymentResponse paymentResponse, RedirectUrl captureCvcUrl)
        {
            this.PaymentResponse = paymentResponse;
            this.CaptureCvcUrl = captureCvcUrl;
        }

        public bool IsCaptureCvc()
        {
            return CaptureCvcUrl != null;
        }

    }
}
