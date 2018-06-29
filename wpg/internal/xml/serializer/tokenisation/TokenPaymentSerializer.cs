using wpg.domain.card;
using wpg.request.tokenisation;

namespace wpg.@internal.xml.serializer.tokenisation
{
    public class TokenPaymentSerializer
    {

        public static void decorateOrder(XmlBuildParams buildParams, TokenPaymentRequest request)
        {
            XmlBuilder builder = buildParams.Builder;

            string scope = request.Scope.ToString().ToLower();

            builder.e("paymentDetails")
                    .e("TOKEN-SSL")
                        .a("tokenScope", scope);

            if (request.CaptureCvc)
            {
                builder.a("captureCvc", "true");
            }

            builder.e("paymentTokenID").cdata(request.PaymentTokenId).up();

            // Add instrument override (currently supports card only)
            decorateInstrumentOverride(buildParams, request);

            // reset to order element
            builder.up().up();
        }

        private static void decorateInstrumentOverride(XmlBuildParams buildParams, TokenPaymentRequest request)
        {
            CardDetails cardDetails = request.CardDetails;
            if (cardDetails != null && cardDetails.isAnythingDefined())
            {
                decorateCardInstrumentOverride(buildParams, cardDetails);
            }
        }

        private static void decorateCardInstrumentOverride(XmlBuildParams buildParams, CardDetails cardDetails)
        {
            XmlBuilder builder = buildParams.Builder;

            builder.e("paymentInstrument").e("cardDetails");

            // Support docs doesn't mention cardNumber, but DTD suggests it's valid
            if (!string.IsNullOrWhiteSpace(cardDetails.CardNumber))
            {
                builder.e("cardNumber").cdata(cardDetails.CardNumber).up();
            }

            if (cardDetails.ExpiryMonth != null && cardDetails.ExpiryYear != null)
            {
                builder.e("expiryDate")
                        .e("date")
                        .a("month", cardDetails.ExpiryMonth.ToString())
                        .a("year", cardDetails.ExpiryYear.ToString())
                        .up().up();
            }

            if (!string.IsNullOrWhiteSpace(cardDetails.CardHolderName))
            {
                builder.e("cardHolderName").cdata(cardDetails.CardHolderName).up();
            }

            if (!string.IsNullOrWhiteSpace(cardDetails.CVC))
            {
                builder.e("cvc").cdata(cardDetails.CVC).up();
            }

            if (cardDetails.CardHolderAddress != null)
            {
                builder.e("cardAddress");
                AddressSerializer.decorateCurrentElement(buildParams, cardDetails.CardHolderAddress);
                builder.up();
            }

            builder.up().up();
        }

    }
}
