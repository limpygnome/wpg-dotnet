using System;
using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;
using Worldpay.@internal.xml.serializer;

namespace Worldpay
{
    public class HostedPaymentPagesRequest : XmlRequest<RedirectUrl>
    {

        public HostedPaymentPagesRequest() : this(null, null, null, null, null)
        {
        }

        public HostedPaymentPagesRequest(OrderDetails orderDetails) : this(orderDetails, null, null, null, null)
        {
            this.OrderDetails = orderDetails;
        }

        public HostedPaymentPagesRequest(OrderDetails orderDetails, Shopper shopper) : this(orderDetails, shopper, null, null, null)
        {
            this.OrderDetails = orderDetails;
            this.Shopper = shopper;
        }

        public HostedPaymentPagesRequest(OrderDetails orderDetails, Shopper shopper, Address billingAddress, Address shippingAddress, PaymentMethodTypeFilter filter)
        {
            this.Filter = new PaymentMethodTypeFilter();
            this.OrderDetails = orderDetails;
            this.Shopper = shopper;
            this.BillingAddress = billingAddress;
            this.ShippingAddress = shippingAddress;
            this.Filter = filter;
        }

        // Mandatory
        public OrderDetails OrderDetails { get; set; }

        // Optional
        public Shopper Shopper { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public PaymentMethodTypeFilter Filter { get; private set; }

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notNull(OrderDetails, "Order details are mandatory");
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            OrderDetailsSerializer.decorateAndStartOrder(buildParams, OrderDetails);
            PaymentMethodTypeMaskSerializer.decorate(buildParams, Filter);
            ShopperSerializer.decorateOrder(buildParams, Shopper);
            AddressSerializer.decorateOrder(buildParams, BillingAddress, ShippingAddress);
            OrderDetailsSerializer.decorateFinishOrder(buildParams);
        }

        internal override RedirectUrl Adapt(XmlResponse response)
        {
            RedirectUrlXmlAdapter adapter = new RedirectUrlXmlAdapter();
            RedirectUrl redirectUrl = adapter.read(response);
            return redirectUrl;
        }

    }
}
