using System.Collections.Generic;
using wpg.domain.tokenisation;
using wpg.@internal.validation;
using wpg.@internal.xml;
using wpg.@internal.xml.adapter;
using wpg.@internal.xml.serializer.tokenisation;

namespace wpg.request.tokenisation
{
    public class FetchTokensByShopperRequest : XmlRequest<List<Token>>
    {

        public string ShopperId { get; set; }

        public FetchTokensByShopperRequest() { }

        public FetchTokensByShopperRequest(string shopperId)
        {
            this.ShopperId = shopperId;
        }

        protected override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(ShopperId, "Shopper ID is mandatory");
        }

        protected override void Build(XmlBuildParams buildParams)
        {
            FetchTokenSerializer.decorate(buildParams, this);
        }

        protected override List<Token> Adapt(XmlResponse response)
        {
            List<Token> result = TokenInquiryAdapter.readShopperTokens(response);
            return result;
        }

    }
}
