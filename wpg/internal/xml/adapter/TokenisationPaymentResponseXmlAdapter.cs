namespace Worldpay.@internal.xml.adapter
{
    internal class TokenisationPaymentResponseXmlAdapter
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
