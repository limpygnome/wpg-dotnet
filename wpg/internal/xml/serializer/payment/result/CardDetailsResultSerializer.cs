namespace Worldpay.@internal.xml.serializer.payment.result
{
    internal class CardDetailsResultSerializer
    {

        public static CardDetailsResult read(XmlBuilder builder)
        {
            string maskedCardNumber = null;
            string hashedCardNumber = null;
            long? expiryMonth = null;
            long? expiryYear = null;

            CardType? type = null;

            // payment detail (currently cards only) (not always enabled)
            if (builder.hasE("paymentMethodDetail"))
            {
                if (builder.hasE("card"))
                {
                    maskedCardNumber = builder.a("number");
                    hashedCardNumber = builder.a("hash");

                    if (builder.hasE("expiryDate"))
                    {
                        if (builder.hasE("date"))
                        {
                            expiryMonth = builder.aToLong("month");
                            expiryYear = builder.aToLong("year");
                            builder.up();
                        }
                        builder.up();
                    }

                    string rawType = builder.a("type");
                    if (rawType != null)
                    {
                        switch (rawType)
                        {
                            case "creditcard":
                                type = CardType.CREDIT;
                                break;
                            case "debitcard":
                                type = CardType.DEBIT;
                                break;
                        }
                    }

                    builder.up();
                }
                builder.up();
            }

            // issuer
            string issuerCountryCode = builder.getCdata("issuerCountryCode");
            string issuerName = builder.getCdata("issuerName");
            string cardHolderName = builder.getCdata("cardHolderName");

            // Check whether anything was found, otherwise return no result at all
            CardDetailsResult result;

            if (maskedCardNumber != null || hashedCardNumber != null || expiryMonth != null || expiryYear != null
                    || issuerCountryCode != null || issuerName != null || cardHolderName != null
                    || type != null)
            {
                result = new CardDetailsResult(maskedCardNumber, hashedCardNumber, expiryMonth, expiryYear,
                        issuerCountryCode, issuerName, cardHolderName, type);
            }
            else
            {
                result = null;
            }

            return result;
        }

    }
}
