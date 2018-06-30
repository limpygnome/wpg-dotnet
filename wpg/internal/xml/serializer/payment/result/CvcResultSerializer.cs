namespace Worldpay.@internal.xml.serializer.payment.result
{
    internal class CvcResultSerializer
    {

        public static CvcResult read(XmlBuilder builder)
        {
            CvcResult result = null;
            if (builder.hasE("CVCResultCode"))
            {
                string resultCode = builder.a("description");
                result = new CvcResult(resultCode);
                builder.up();
            }
            return result;
        }

    }
}
