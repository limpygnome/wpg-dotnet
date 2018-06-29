using wpg.domain;
using wpg.domain.card;
using wpg.domain.tokenisation;
using wpg.@internal.validation;
using wpg.@internal.xml;
using wpg.@internal.xml.adapter;
using wpg.@internal.xml.serializer;
using wpg.@internal.xml.serializer.tokenisation;

namespace wpg.request.tokenisation
{
    public class TokenPaymentRequest : XmlRequest<TokenisationPaymentResponse>
    {

        // Mandatory
        public string PaymentTokenId { get; set; }
        public TokenScope Scope { get; set; }
        public OrderDetails OrderDetails { get; set; }

        // Partially mandatory
        public Shopper Shopper { get; set; }

        // Optional
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public bool CaptureCvc { get; set; }
        public CardDetails CardDetails { get; set; }

        public TokenPaymentRequest() : this(null, TokenScope.SHOPPER, null, null, null, null, false)
        {
        }

        public TokenPaymentRequest(string paymentTokenId, OrderDetails orderDetails) : this(paymentTokenId, TokenScope.MERCHANT, orderDetails, null, null, null, false)
        {
        }

        public TokenPaymentRequest(string paymentTokenId, OrderDetails orderDetails, Shopper shopper) : this(paymentTokenId, TokenScope.SHOPPER, orderDetails, shopper, null, null, false)
        {
        }

        public TokenPaymentRequest(string paymentTokenId, TokenScope scope, OrderDetails orderDetails, Shopper shopper, bool captureCvc) : this(paymentTokenId, scope, orderDetails, shopper, null, null, captureCvc)
        {
        }

        public TokenPaymentRequest(string paymentTokenId, TokenScope scope, OrderDetails orderDetails, Shopper shopper, Address billingAddress, Address shippingAddress, bool captureCvc)
        {
            this.PaymentTokenId = paymentTokenId;
            this.Scope = scope;
            this.OrderDetails = orderDetails;
            this.Shopper = shopper;
            this.BillingAddress = billingAddress;
            this.ShippingAddress = shippingAddress;
            this.CaptureCvc = captureCvc;
        }

        protected override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(PaymentTokenId, "Payment token ID is mandatory");
            Assert.notNull(Scope, "Token scope is mandatory");
            Assert.notNull(OrderDetails, "Order details are mandatory");

            if (Scope == TokenScope.SHOPPER)
            {
                Assert.notNull(Shopper, "Shopper ID, using shopper, is mandatory for shopper tokens");
                Assert.notEmpty(Shopper.ShopperId, "Shopper ID, using shopper, is mandatory for shopper tokens\"");
            }
        }

        protected override void Build(XmlBuildParams buildParams)
        {
            OrderDetailsSerializer.decorateAndStartOrder(buildParams, OrderDetails);
            TokenPaymentSerializer.decorateOrder(buildParams, this);
            SessionSerializer.decorateOrderPaymentDetails(buildParams, Shopper);
            ShopperSerializer.decorateOrder(buildParams, Shopper);
            AddressSerializer.decorateOrder(buildParams, BillingAddress, ShippingAddress);
            OrderDetailsSerializer.decorateFinishOrder(buildParams);
        }

        protected override TokenisationPaymentResponse Adapt(XmlResponse response)
        {
            TokenisationPaymentResponseXmlAdapter adapter = new TokenisationPaymentResponseXmlAdapter();
            TokenisationPaymentResponse paymentResponse = adapter.read(response);
            return paymentResponse;
        }

    }
}
