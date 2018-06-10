using System;
using wpg.domain.payment.result;

namespace wpg.@internal.xml.serializer.payment.result
{
    public class AvsResultSerializer
    {

        public static AvsResult read(XmlBuilder builder)
        {
            AvsResult result = null;
            if (builder.hasE("AVSResultCode"))
            {
                String resultCode = builder.a("description");
                result = new AvsResult(resultCode);
                builder.up();
            }
            return result;
        }

    }
}
