namespace Worldpay.@internal.xml.serializer
{
    internal class OrderInquirySerializer
    {

        public static void decorate(XmlBuildParams buildParams, OrderInquiryRequest request)
        {
            string orderCode = request.OrderCode;

            XmlBuilder builder = buildParams.Builder;
            builder.e("inquiry")
                    .e("orderInquiry").a("orderCode", orderCode);
        }

    }
}
