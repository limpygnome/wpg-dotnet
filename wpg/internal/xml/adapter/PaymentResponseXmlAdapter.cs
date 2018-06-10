using System;
using wpg.connection.http;
using wpg.domain.payment;
using wpg.domain.payment.threeds;
using wpg.exception;
using wpg.@internal.xml.serializer.payment;
using wpg.@internal.xml.serializer.payment.threeds;

namespace wpg.@internal.xml.adapter
{
    public class PaymentResponseXmlAdapter
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
                    result = readOrderStatus(httpResponse, builder);
                }
            }

            if (result == null)
            {
                throw new WpgMalformedException(httpResponse);
            }

            return result;
        }

        private PaymentResponse readOrderStatus(HttpResponse httpResponse, XmlBuilder builder)
        {
            PaymentResponse result = null;

            if (builder.hasE("requestInfo"))
            {
                if (builder.hasE("request3DSecure"))
                {
                    ThreeDsDetails threeDsDetails = ThreeDsSerializer.read(builder);
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
