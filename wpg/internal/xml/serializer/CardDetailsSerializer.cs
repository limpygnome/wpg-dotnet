using System;
using wpg.domain;
using wpg.domain.card;

namespace wpg.@internal.xml.serializer
{
    public class CardDetailsSerializer
    {

        public static void decorateOrder(XmlBuildParams buildParams, CardDetails cardDetails)
        {
            if (cardDetails != null)
            {
                XmlBuilder builder = buildParams.Builder;

                builder.e("paymentDetails");

                // build card element
                // TODO allow card scheme to be customised?
                builder.e("CARD-SSL")
                        .e("cardNumber")
                            .cdata(cardDetails.CardNumber)
                            .up()
                        .e("expiryDate")
                            .e("date")
                                .a("month", cardDetails.ExpiryMonth.ToString())
                                .a("year", cardDetails.ExpiryYear.ToString())
                                .up()
                            .up()
                        .e("cardHolderName")
                            .cdata(cardDetails.CardHolderName)
                            .up();

                // cvc
                String cvc = cardDetails.CVC;
                if (!String.IsNullOrWhiteSpace(cvc))
                {
                    builder.e("cvc").cdata(cvc).up();
                }

                // add card holder address
                if (cardDetails.CardHolderAddress != null)
                {
                    builder.e("cardAddress");
                    AddressSerializer.decorateCurrentElement(buildParams, cardDetails.CardHolderAddress);
                    builder.up();
                }

                // reset to order element
                builder.up().up();
            }
        }

        public static CardDetails read(XmlBuilder builder)
        {
            String cardNumber = builder.getCdata("cardNumber");
            String cardHolderName = builder.getCdata("cardHolderName");
            String cvc = builder.getCdata("cvc");
            String encryptedPAN = builder.getCdata("encryptedPAN");
            long? expiryMonth = null;
            long? expiryYear = null;

            if (builder.hasE("expiryDate"))
            {
                if (builder.hasE("date"))
                {
                    try
                    {
                        expiryMonth = Int64.Parse(builder.a("month"));
                        expiryYear = Int64.Parse(builder.a("year"));
                    }
                    catch (FormatException)
                    {
                    }
                    builder.up();
                }
                builder.up();
            }

            Address cardHolderAddress = null;
            if (builder.hasE("cardAddress"))
            {
                cardHolderAddress = AddressSerializer.read(builder);
                builder.up();
            }

            /*
                Only return results if we actually parsed something; tokenisation returns an empty instance, with empty date.
                Thus we don't want to give merchants an empty object.
             */

            bool hasDetails = (cardNumber != null || cardHolderName != null || cvc != null || encryptedPAN != null || cardHolderAddress != null);
            hasDetails |= (expiryMonth != null && expiryYear != null);

            CardDetails result = null;
            if (hasDetails)
            {
                result = new CardDetails(
                        cardNumber, expiryMonth, expiryYear, cardHolderName, cvc, cardHolderAddress, encryptedPAN);
            }

            return result;
        }


    }
}
