using Worldpay.@internal.xml.serializer.payment;
using Worldpay.@internal.xml.serializer.payment.threeds;

namespace Worldpay.@internal.xml.adapter
{
    internal class PaymentResponseXmlAdapter
    {

        public PaymentResponse read(XmlResponse response)
        {
            HttpResponse httpResponse = response.Response;
            XmlBuilder builder = response.Builder;

            PaymentResponse result = null;

            if (builder.isCurrentTag("paymentService"))
            {
                if (builder.hasE("reply") && builder.hasE("orderStatus"))
                {
                    result = readOrderStatus(httpResponse, response.SessionContext, builder);
                }
            }

            if (result == null)
            {
                throw new WpgMalformedException(httpResponse);
            }

            return result;
        }

        private PaymentResponse readOrderStatus(HttpResponse httpResponse, SessionContext sessionContext, XmlBuilder builder)
        {
            PaymentResponse result = null;

            if (builder.hasE("requestInfo"))
            {
                if (builder.hasE("request3DSecure"))
                {
                    ThreeDsDetails threeDsDetails = ThreeDsSerializer.read(sessionContext, builder);
                    result = new PaymentResponse(threeDsDetails);
                }
            }
            else if (builder.hasE("payment"))
            {
                Payment payment = PaymentSerializer.read(builder);
                result = new PaymentResponse(payment);
            }

            // check we have something
            if (result == null)
            {
                throw new WpgErrorResponseException(0, "Unable to handle response from gateway, not recognized", httpResponse);
            }

            return result;
        }

    }
}
