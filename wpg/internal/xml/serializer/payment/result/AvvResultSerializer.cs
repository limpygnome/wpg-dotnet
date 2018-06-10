using System;
using wpg.domain.payment.result;

namespace wpg.@internal.xml.serializer.payment.result
{
    public class AvvResultSerializer
    {

        public static AvvResult read(XmlBuilder builder)
        {
            AvvResult result = null;

            String address = null, postcode = null, cardHolderName = null, telephone = null, email = null;
            if (builder.hasE("AAVAddressResultCode"))
            {
                address = builder.a("description");
                builder.up();
            }
            if (builder.hasE("AAVPostcodeResultCode"))
            {
                postcode = builder.a("description");
                builder.up();
            }
            if (builder.hasE("AAVCardholderNameResultCode"))
            {
                cardHolderName = builder.a("description");
                builder.up();
            }
            if (builder.hasE("AAVTelephoneResultCode"))
            {
                telephone = builder.a("description");
                builder.up();
            }
            if (builder.hasE("AAVEmailResultCode"))
            {
                email = builder.a("description");
                builder.up();
            }

            if (address != null || postcode != null || cardHolderName != null || telephone != null || email != null)
            {
                result = new AvvResult(address, postcode, cardHolderName, telephone, email);
            }

            return result;
        }

    }
}
