namespace Worldpay.@internal.xml.serializer.tokenisation
{
    internal class FetchTokenSerializer
    {

        public static void decorate(XmlBuildParams buildParams, FetchTokenRequest request)
        {
            XmlBuilder builder = buildParams.Builder;

            string scope = request.Scope.ToString().ToLower();
            string shopperId = request.ShopperId;
            string paymentTokenId = request.PaymentTokenId;

            builder.e("inquiry")
                    .e("paymentTokenInquiry").a("tokenScope", scope);

            if (shopperId != null)
            {
                builder.e("authenticatedShopperID").cdata(shopperId).up();
            }

            builder.e("paymentTokenID").cdata(paymentTokenId).up();
        }

        public static void decorate(XmlBuildParams buildParams, FetchTokensByShopperRequest request)
        {
            XmlBuilder builder = buildParams.Builder;

            string shopperId = request.ShopperId;

            builder.e("inquiry")
                    .e("shopperTokenRetrieval")
                    .e("authenticatedShopperID").cdata(shopperId).up();
        }

    }
}
