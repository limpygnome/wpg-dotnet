using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.serializer.tokenisation;

namespace Worldpay
{
    public class DeleteTokenRequest : XmlRequest<Void>
    {

        // Mandatory
        public string PaymentTokenId { get; set; }
        public TokenScope Scope { get; set; }

        // Partially mandatory
        public string ShopperId { get; set; }

        // Optional
        public string EventReference { get; set; }
        public string Reason { get; set; }

        public DeleteTokenRequest(string paymentTokenId)
        {
            this.PaymentTokenId = paymentTokenId;
            this.Scope = TokenScope.MERCHANT;
        }

        public DeleteTokenRequest(string paymentTokenId, string shopperId)
        {
            this.PaymentTokenId = paymentTokenId;
            this.ShopperId = shopperId;
            this.Scope = (shopperId != null ? TokenScope.SHOPPER : TokenScope.MERCHANT);
        }

        public DeleteTokenRequest(string paymentTokenId, string shopperId, string eventReference, string reason, TokenScope scope)
        {
            this.PaymentTokenId = paymentTokenId;
            this.ShopperId = shopperId;
            this.EventReference = eventReference;
            this.Reason = reason;
            this.Scope = scope;
        }

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(PaymentTokenId, "Payment token ID is mandatory");
            Assert.notNull(Scope, "Token scope is mandatory");

            if (Scope == TokenScope.SHOPPER)
            {
                Assert.notEmpty(ShopperId, "Shopper ID is mandatory for shopper tokens");
            }
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            DeleteTokenSerializer.decorate(buildParams, this);
        }

        internal override Void Adapt(XmlResponse response)
        {
            XmlBuilder builder = response.Builder;

            if (!builder.hasE("reply") || !builder.hasE("ok") || !builder.hasE("deleteTokenReceived"))
            {
                throw new WpgMalformedException(response.Response);
            }

            return new Void();
        }

    }
}
