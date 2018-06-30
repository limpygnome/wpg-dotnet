namespace Worldpay.@internal.xml.serializer.payment.threeds
{
    internal class ThreeDsSerializer
    {

        public static ThreeDsDetails read(SessionContext sessionContext, XmlBuilder builder)
        {
            string issuerURL = builder.getCdata("issuerURL");
            string paRequest = builder.getCdata("paRequest");

            ThreeDsDetails threeDsDetails = new ThreeDsDetails(sessionContext, issuerURL, paRequest);
            return threeDsDetails;
        }

    }
}
