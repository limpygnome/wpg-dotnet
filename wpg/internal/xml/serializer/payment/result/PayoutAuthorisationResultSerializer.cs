using System;
using wpg.domain.payment.result;

namespace wpg.@internal.xml.serializer.payment.result
{
    public class PayoutAuthorisationResultSerializer
    {

        public static PayoutAuthorisationResult read(XmlBuilder builder)
        {
            PayoutAuthorisationResult result = null;

            if (builder.hasE("AuthorisationId"))
            {
                String authorizationId = builder.a("id");
                result = new PayoutAuthorisationResult(authorizationId);
                builder.up();
            }

            return result;
        }

    }
}
