using System;
using wpg.domain;
using wpg.domain.payment;
using wpg.domain.tokenisation;
using wpg.@internal.xml;
using wpg.@internal.xml.serializer;
using wpg.@internal.xml.serializer.payment.tokenisation;

namespace wpg.request.card
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

        public OrderDetails OrderDetails { get; set; }
        public CardDetails CardDetails { get; set; }
        public Shopper Shopper { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public CreateTokenDetails CreateTokenDetails { get; set; }

        protected override void Validate(XmlBuildParams buildParams)
        {
            // TODO add
        }

        protected override void Build(XmlBuildParams buildParams)
        {
            OrderDetailsSerializer.decorateAndStartOrder(buildParams, OrderDetails);
            CardDetailsSerializer.decorateOrder(buildParams, CardDetails);
            SessionSerializer.decorateOrderPaymentDetails(buildParams, Shopper);
            ShopperSerializer.decorateOrder(buildParams, Shopper);
            AddressSerializer.decorateOrder(buildParams, BillingAddress, ShippingAddress);
            CreateTokenDetailsSerializer.decorateOrder(buildParams, CreateTokenDetails);
            OrderDetailsSerializer.decorateFinishOrder(buildParams);
        }

        protected override PaymentResponse Adapt()
        {
            return null;
        }

    }
}
