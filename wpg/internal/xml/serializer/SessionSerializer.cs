using System;
using wpg.domain;
namespace wpg.@internal.xml.serializer
{
    public class SessionSerializer
    {

        public static void decorateOrderPaymentDetails(XmlBuildParams buildParams, Shopper shopper)
        {
            String sessionId = buildParams.SessionContext.SessionId;
            String shopperIpAddress = (shopper != null ? shopper.IpAddress : null);

            XmlBuilder builder = buildParams.Builder;

            builder.e("paymentDetails");

            // build session element
            if (sessionId != null || shopperIpAddress != null)
            {
                builder.e("session");

                if (!String.IsNullOrWhiteSpace(shopperIpAddress))
                {
                    builder.a("shopperIPAddress", shopperIpAddress);
                }
                if (!String.IsNullOrWhiteSpace(sessionId))
                {
                    builder.a("id", sessionId);
                }
                builder.up();
            }

            builder.up();
        }

    }
}
