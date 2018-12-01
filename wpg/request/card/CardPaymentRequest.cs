using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;
using Worldpay.@internal.xml.serializer;
using Worldpay.@internal.xml.serializer.payment.tokenisation;

namespace Worldpay
{
    public class CardPaymentRequest : XmlRequest<PaymentResponse>
    {
        public CardPaymentRequest()
        {
        }

        public CardPaymentRequest(OrderDetails orderDetails, CardDetails cardDetails, Shopper shopper)
        {
            this.OrderDetails = orderDetails;
            this.CardDetails = cardDetails;
            this.Shopper = shopper;
        }

        public CardPaymentRequest(OrderDetails orderDetails, CardDetails cardDetails, Shopper shopper, Address billingAddress, Address shippingAddress)
        {
            this.OrderDetails = orderDetails;
            this.CardDetails = cardDetails;
            this.Shopper = shopper;
            this.BillingAddress = billingAddress;
            this.ShippingAddress = shippingAddress;
        }

        public CardPaymentRequest(OrderDetails orderDetails, CardDetails cardDetails, Shopper shopper, Address billingAddress, Address shippingAddress, CreateTokenDetails createTokenDetails)
        {
            this.OrderDetails = orderDetails;
            this.CardDetails = cardDetails;
            this.Shopper = shopper;
            this.BillingAddress = billingAddress;
            this.ShippingAddress = shippingAddress;
            this.CreateTokenDetails = createTokenDetails;
        }

        // Mandatory
        public OrderDetails OrderDetails { get; set; }
        public CardDetails CardDetails { get; set; }

        // Partially mandatory
        public Shopper Shopper { get; set; }

        // Optional
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public CreateTokenDetails CreateTokenDetails { get; set; }

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notNull(OrderDetails, "Order details are mandatory");
            Assert.notNull(CardDetails, "Card details are mandatory");

            // Shopper tokens always require shopper to be present
            if (CreateTokenDetails != null && TokenScope.SHOPPER == CreateTokenDetails.Scope)
            {
                Assert.notNull(Shopper, "Shopper is required for tokenised payments");
                Assert.notEmpty(Shopper.ShopperId, "Shopper ID is required for tokenised payments");
            }
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            OrderDetailsSerializer.decorateAndStartOrder(buildParams, OrderDetails);
            CardDetailsSerializer.decorateOrder(buildParams, CardDetails);
            SessionSerializer.decorateOrderPaymentDetails(buildParams, Shopper);
            ShopperSerializer.decorateOrder(buildParams, Shopper);
            AddressSerializer.decorateOrder(buildParams, BillingAddress, ShippingAddress);
            CreateTokenDetailsSerializer.decorateOrder(buildParams, CreateTokenDetails);
            OrderDetailsSerializer.decorateFinishOrder(buildParams);
        }

        internal override PaymentResponse Adapt(XmlResponse response)
        {
            PaymentResponseXmlAdapter adapter = new PaymentResponseXmlAdapter();
            PaymentResponse result = adapter.read(response);
            return result;
        }

    }
}
