using wpg.request.inquiry;

namespace wpg.@internal.xml.serializer
{
    public class OrderInquirySerializer
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
