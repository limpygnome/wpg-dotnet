namespace Worldpay.@internal.xml.serializer.payment.result
{
    internal class AvsResultSerializer
    {

        public static AvsResult read(XmlBuilder builder)
        {
            AvsResult result = null;
            if (builder.hasE("AVSResultCode"))
            {
                string resultCode = builder.a("description");
                result = new AvsResult(resultCode);
                builder.up();
            }
            return result;
        }

    }
}
