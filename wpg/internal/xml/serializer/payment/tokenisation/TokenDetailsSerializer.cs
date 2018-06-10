using System;
using wpg.domain.tokenisation;

namespace wpg.@internal.xml.serializer.payment.tokenisation
{
    public class TokenDetailsSerializer
    {

        public static TokenDetails read(XmlBuilder builder)
        {
            TokenDetails tokenDetails = null;

            if (builder.hasE("tokenDetails"))
            {
                string tokenEvent = builder.a("tokenEvent");
                string paymentTokenId = builder.getCdata("paymentTokenID");

                builder.e("paymentTokenExpiry");
                DateTime tokenExpiry = DateSerializer.readDateTime(builder);
                builder.up();

                string eventReference = builder.getCdata("tokenEventReference");
                string eventReason = builder.getCdata("tokenReason");

                tokenDetails = new TokenDetails(paymentTokenId, tokenExpiry, tokenEvent, eventReference, eventReason);
                builder.up();
            }

            return tokenDetails;
        }

    }
}
