using System;
using wpg.domain.payment.result;

namespace wpg.@internal.xml.serializer.payment.result
{
    public class ISO8583ResultSerializer
    {

        public static ISO8583Result read(XmlBuilder builder)
        {
            ISO8583Result result = null;

            if (builder.hasE("ISO8583ReturnCode"))
            {
                String code = builder.a("code");
                String description = builder.a("description");
                result = new ISO8583Result(code, description);
                builder.up();
            }

            return result;
        }

    }
}
