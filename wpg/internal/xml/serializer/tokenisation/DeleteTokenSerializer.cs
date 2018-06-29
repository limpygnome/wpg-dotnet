using wpg.request.tokenisation;

namespace wpg.@internal.xml.serializer.tokenisation
{
    public class DeleteTokenSerializer
    {

        public static void decorate(XmlBuildParams buildParams, DeleteTokenRequest request)
        {
            XmlBuilder builder = buildParams.Builder;

            string scope = request.Scope.ToString().ToLower();
            string shopperId = request.ShopperId;
            string eventReference = request.EventReference;
            string reason = request.Reason;

            builder.e("modify")
                    .e("paymentTokenDelete").a("tokenScope", scope)
                   .e("paymentTokenID").cdata(request.PaymentTokenId).up();

            if (shopperId != null)
            {
                builder.e("authenticatedShopperID").cdata(request.ShopperId).up();
            }

            if (eventReference != null)
            {
                builder.e("tokenEventReference").cdata(request.EventReference).up();
            }

            if (reason != null)
            {
                builder.e("tokenReason").cdata(request.Reason).up();
            }
        }

    }
}
