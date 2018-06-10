using System;
using wpg.domain.payment.result;

namespace wpg.@internal.xml.serializer.payment.result
{
    public class ThreeDSecureResultSerializer
    {

        public static ThreeDSecureResult read(XmlBuilder builder)
        {
            ThreeDSecureResult result = null;

            if (builder.hasE("ThreeDSecureResult"))
            {
                String description = builder.a("description");
                String eci = builder.getCdata("eci");
                String cavv = builder.getCdata("cavv");

                result = new ThreeDSecureResult(description, eci, cavv);
                builder.up();
            }

            return result;
        }

    }
}
