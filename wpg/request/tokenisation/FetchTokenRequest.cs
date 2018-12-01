using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;
using Worldpay.@internal.xml.serializer.tokenisation;

namespace Worldpay
{
    public class FetchTokenRequest : XmlRequest<Token>
    {
        
        public string PaymentTokenId { get; set; }
        public TokenScope Scope { get; set; }
        public string ShopperId { get; set; }
        public bool Detokenise { get; set; }

        public FetchTokenRequest(string paymentTokenId) : this(paymentTokenId, null, TokenScope.MERCHANT, false)
        {
        }

        public FetchTokenRequest(string paymentTokenId, string shopperId) : this(paymentTokenId, shopperId, TokenScope.SHOPPER, false)
        {
        }

        public FetchTokenRequest(string paymentTokenId, string shopperId, TokenScope scope, bool detokenise)
        {
            this.PaymentTokenId = paymentTokenId;
            this.ShopperId = shopperId;
            this.Scope = scope;
            this.Detokenise = detokenise;
        }

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(PaymentTokenId, "Payment token ID is mandatory");
            Assert.notNull(Scope, "Scope is mandatory");

            if (Scope == TokenScope.SHOPPER)
            {
                Assert.notEmpty(ShopperId, "Shopper ID is required for shopper tokens");
            }
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            FetchTokenSerializer.decorate(buildParams, this);
        }

        internal override Token Adapt(XmlResponse response)
        {
            Token token = TokenInquiryAdapter.readToken(response);
            return token;
        }

    }
}
