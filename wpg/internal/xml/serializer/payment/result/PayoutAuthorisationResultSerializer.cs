namespace Worldpay.@internal.xml.serializer.payment.result
{
    internal class PayoutAuthorisationResultSerializer
    {

        public static PayoutAuthorisationResult read(XmlBuilder builder)
        {
            PayoutAuthorisationResult result = null;

            if (builder.hasE("AuthorisationId"))
            {
                string authorizationId = builder.a("id");
                result = new PayoutAuthorisationResult(authorizationId);
                builder.up();
            }

            return result;
        }

    }
}
