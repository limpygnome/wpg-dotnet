using System;
using wpg.domain.payment.result;

namespace wpg.@internal.xml.serializer.payment.result
{
    public class CvcResultSerializer
    {

        public static CvcResult read(XmlBuilder builder)
        {
            CvcResult result = null;
            if (builder.hasE("CVCResultCode"))
            {
                String resultCode = builder.a("description");
                result = new CvcResult(resultCode);
                builder.up();
            }
            return result;
        }

    }
}
