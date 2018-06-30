using System.Collections.Generic;
using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;
using Worldpay.@internal.xml.serializer.tokenisation;

namespace Worldpay
{
    public class FetchTokensByShopperRequest : XmlRequest<List<Token>>
    {

        public string ShopperId { get; set; }

        public FetchTokensByShopperRequest() { }

        public FetchTokensByShopperRequest(string shopperId)
        {
            this.ShopperId = shopperId;
        }

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(ShopperId, "Shopper ID is mandatory");
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            FetchTokenSerializer.decorate(buildParams, this);
        }

        internal override List<Token> Adapt(XmlResponse response)
        {
            List<Token> result = TokenInquiryAdapter.readShopperTokens(response);
            return result;
        }

    }
}
