using System;
using wpg.domain.payment.threeds;

namespace wpg.@internal.xml.serializer.payment.threeds
{
    public class ThreeDsSerializer
    {

        public static ThreeDsDetails read(XmlBuilder builder)
        {
            string issuerURL = builder.getCdata("issuerURL");
            string paRequest = builder.getCdata("paRequest");

            ThreeDsDetails threeDsDetails = new ThreeDsDetails(issuerURL, paRequest);
            return threeDsDetails;
        }

    }
}
