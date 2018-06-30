using System;

namespace Worldpay.@internal.xml.serializer.payment.tokenisation
{
    internal class CreateTokenDetailsSerializer
    {

        public static void decorateOrder(XmlBuildParams buildParams, CreateTokenDetails createTokenDetails)
        {
            XmlBuilder builder = buildParams.Builder;

            if (createTokenDetails != null)
            {
                TokenScope scope = createTokenDetails.Scope;
                builder.e("createToken").a("tokenScope", scope.ToString().ToLower());
                builder.e("tokenEventReference").cdata(createTokenDetails.EventReference).up();
                builder.e("tokenReason").cdata(createTokenDetails.Reason).up();

                if (createTokenDetails.ShortLifeMins != null && createTokenDetails.ShortLifeMins > 0)
                {
                    builder.e("shortLifeMins").cdata(createTokenDetails.ShortLifeMins.ToString());
                }
                else if (createTokenDetails.Expiry != null)
                {
                    builder.e("paymentTokenExpiry");

                    DateTime dateTime = (DateTime) createTokenDetails.Expiry;
                    DateSerializer.writeDateTime(builder, dateTime);

                    builder.up();
                }

                // reset to order
                builder.up();
            }
        }

    }
}
