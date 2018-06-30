namespace Worldpay.@internal.xml.serializer.payment.tokenisation
{
    internal class TokenSerializer
    {

        public static Token read(XmlBuilder builder)
        {
            Token token = null;

            if (builder.hasE("token"))
            {
                string shopperId = builder.getCdata("authenticatedShopperID");
                TokenDetails details = TokenDetailsSerializer.read(builder);
                TokenInstrument instrument = TokenInstrumentSerializer.read(builder);

                token = new Token(details, instrument, shopperId);
            }

            return token;
        }

    }
}
