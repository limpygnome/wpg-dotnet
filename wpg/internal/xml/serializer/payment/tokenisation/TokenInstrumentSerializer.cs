using System;
using wpg.domain.payment.result;
using wpg.domain.tokenisation;
using wpg.@internal.xml.serializer.payment.result;

namespace wpg.@internal.xml.serializer.payment.tokenisation
{
    public class TokenInstrumentSerializer
    {

        public static TokenInstrument read(XmlBuilder builder)
        {
            TokenInstrument result = null;

            if (builder.hasE("paymentInstrument"))
            {
                if (builder.hasE("cardDetails"))
                {
                    result = readCardDetails(builder);
                    builder.up();
                }
                builder.up();
            }

            return result;
        }

        public static TokenInstrument readCardDetails(XmlBuilder builder)
        {
            CardDetailsResult cardDetailsResult = CardDetailsResultSerializer.read(builder);
            String cardBrand = null;
            String cardSubBrand = null;
            String issuerCountryCode = null;
            String obfuscatedPAN = null;

            if (builder.hasE("derived"))
            {
                cardBrand = builder.getCdata("cardBrand");
                cardSubBrand = builder.getCdata("cardSubBrand");
                issuerCountryCode = builder.getCdata("issuerCountryCode");
                obfuscatedPAN = builder.getCdata("obfuscatedPAN");

                builder.up();
            }

            TokenInstrument result = new TokenCardDetails(cardBrand, cardSubBrand, issuerCountryCode, obfuscatedPAN, cardDetailsResult);
            return result;
        }

    }
}
