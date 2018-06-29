using wpg.domain;
using wpg.domain.payment;
using wpg.@internal.validation;
using wpg.@internal.xml;
using wpg.@internal.xml.adapter;
using wpg.@internal.xml.serializer;

namespace wpg.request.inquiry
{
    public class OrderInquiryRequest : XmlRequest<Payment>
    {

        public string OrderCode { get; set; }

        public OrderInquiryRequest() { }

        public OrderInquiryRequest(OrderDetails orderDetails)
        {
            Assert.notNull(orderDetails, "Order details is null");
            this.OrderCode = orderDetails.OrderCode;
        }

        public OrderInquiryRequest(string orderCode)
        {
            this.OrderCode = orderCode;
        }

        protected override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(OrderCode, "Order code is mandatory");
        }

        protected override void Build(XmlBuildParams buildParams)
        {
            OrderInquirySerializer.decorate(buildParams, this);
        }

        protected override Payment Adapt(XmlResponse response)
        {
            PaymentResponseXmlAdapter adapter = new PaymentResponseXmlAdapter();
            PaymentResponse result = adapter.read(response);
            Payment payment = result.Payment;
            return payment;
        }

    }
}
