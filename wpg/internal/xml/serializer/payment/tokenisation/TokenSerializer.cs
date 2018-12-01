namespace Worldpay.@internal.xml.serializer.payment.tokenisation
{
    internal class TokenSerializer
    {

        public static Token readElement(XmlBuilder builder)
        {
            Token token = null;
            if (builder.hasE("token"))
            {
                token = read(builder);
            }
            return token;
        }

        public static Token read(XmlBuilder builder)
        {
            Token token = null;

            string shopperId = builder.getCdata("authenticatedShopperID");
            TokenDetails details = TokenDetailsSerializer.read(builder);
            TokenInstrument instrument = TokenInstrumentSerializer.read(builder);

            if (shopperId != null || details != null || instrument != null)
            {
                token = new Token(details, instrument, shopperId);
            }

            return token;
        }

    }
}
