using wpg.domain.payment;
using wpg.domain.redirect;
using wpg.domain.tokenisation;
using wpg.exception;

namespace wpg.@internal.xml.adapter
{
    public class TokenisationPaymentResponseXmlAdapter
    {

        public TokenisationPaymentResponse read(XmlResponse response)
        {
            XmlBuilder builder = response.Builder;

            PaymentResponse paymentResponse = null;
            RedirectUrl captureCvc = null;

            // Attempt to parse
            if (builder.hasE("reply") && builder.hasE("orderStatus") && builder.hasE("reference"))
            {
                RedirectUrlXmlAdapter adapter = new RedirectUrlXmlAdapter();
                captureCvc = adapter.read(response);
            }
            else
            {
                builder.reset();

                PaymentResponseXmlAdapter adapter = new PaymentResponseXmlAdapter();
                paymentResponse = adapter.read(response);
            }

            if (captureCvc == null && paymentResponse == null)
            {
                throw new WpgMalformedException(response.Response);
            }

            TokenisationPaymentResponse result = new TokenisationPaymentResponse(paymentResponse, captureCvc);
            return result;
        }

    }
}
