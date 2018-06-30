using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;
using Worldpay.@internal.xml.serializer;

namespace Worldpay
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

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(OrderCode, "Order code is mandatory");
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            OrderInquirySerializer.decorate(buildParams, this);
        }

        internal override Payment Adapt(XmlResponse response)
        {
            PaymentResponseXmlAdapter adapter = new PaymentResponseXmlAdapter();
            PaymentResponse result = adapter.read(response);
            Payment payment = result.Payment;
            return payment;
        }

    }
}
