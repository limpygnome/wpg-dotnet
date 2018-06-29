using wpg.domain.tokenisation;
using wpg.@internal.validation;
using wpg.@internal.xml;
using wpg.@internal.xml.serializer.payment.tokenisation;
using wpg.@internal.xml.serializer.tokenisation;

namespace wpg.request.tokenisation
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

        protected override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(PaymentTokenId, "Payment token ID is mandatory");
            Assert.notNull(Scope, "Scope is mandatory");

            if (Scope == TokenScope.SHOPPER)
            {
                Assert.notEmpty(ShopperId, "Shopper ID is required for shopper tokens");
            }
        }

        protected override void Build(XmlBuildParams buildParams)
        {
            FetchTokenSerializer.decorate(buildParams, this);
        }

        protected override Token Adapt(XmlResponse response)
        {
            XmlBuilder builder = response.Builder;
            Token token = TokenSerializer.read(builder);
            return token;
        }

    }
}
