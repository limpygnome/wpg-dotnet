namespace Worldpay.@internal.xml.serializer.payment.result
{
    internal class ThreeDSecureResultSerializer
    {

        public static ThreeDSecureResult read(XmlBuilder builder)
        {
            ThreeDSecureResult result = null;

            if (builder.hasE("ThreeDSecureResult"))
            {
                string description = builder.a("description");
                string eci = builder.getCdata("eci");
                string cavv = builder.getCdata("cavv");

                result = new ThreeDSecureResult(description, eci, cavv);
                builder.up();
            }

            return result;
        }

    }
}
