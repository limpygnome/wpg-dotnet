namespace Worldpay.@internal.xml.serializer.payment.result
{
    internal class ISO8583ResultSerializer
    {

        public static ISO8583Result read(XmlBuilder builder)
        {
            ISO8583Result result = null;

            if (builder.hasE("ISO8583ReturnCode"))
            {
                string code = builder.a("code");
                string description = builder.a("description");
                result = new ISO8583Result(code, description);
                builder.up();
            }

            return result;
        }

    }
}
