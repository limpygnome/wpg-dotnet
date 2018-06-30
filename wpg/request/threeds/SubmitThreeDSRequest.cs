using System;
using System.Threading.Tasks;
using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;

namespace Worldpay
{
    public class SubmitThreeDSRequest : XmlRequest<PaymentResponse>
    {

        public SubmitThreeDSRequest()
        {
        }
    
        public SubmitThreeDSRequest(string orderCode, string paResponse)
        {
            this.OrderCode = orderCode;
            this.PaResponse = paResponse;
        }

        public SubmitThreeDSRequest(OrderDetails orderDetails, string paResponse)
        {
            this.OrderCode = orderDetails.OrderCode;
            this.PaResponse = paResponse;
        }

        public SubmitThreeDSRequest(SessionContext sessionContext, OrderDetails orderDetails, string paResponse)
        {
            this.SessionContext = sessionContext;
            this.OrderCode = orderDetails.OrderCode;
            this.PaResponse = paResponse;
        }

        public SubmitThreeDSRequest(ThreeDsDetails threeDsDetails, OrderDetails orderDetails, string paResponse)
        {
            this.SessionContext = threeDsDetails.SessionContext;
            this.OrderCode = orderDetails.OrderCode;
            this.PaResponse = paResponse;
        }

        public SessionContext SessionContext { get; private set; }
        public string OrderCode { get; set; }
        public string PaResponse { get; set; }

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notEmpty(OrderCode, "Order code is mandatory");
            Assert.notEmpty(PaResponse, "PaResponse is mandatory");
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            XmlBuilder builder = buildParams.Builder;

            UserPassAuth auth = (UserPassAuth) buildParams.GatewayContext.Auth;
            String sessionId = buildParams.SessionContext.SessionId;

            builder.a("merchantCode", auth.MerchantCode)
                    .e("submit")
                   .e("order").a("orderCode", OrderCode);

            if (!String.IsNullOrWhiteSpace(auth.InstallationId))
            {
                builder.a("installationId", auth.InstallationId);
            }

            builder
                    .e("info3DSecure")
                        .e("paResponse").cdata(PaResponse).up()
                    .up()
                    .e("session").a("id", sessionId);
        }

        internal override PaymentResponse Adapt(XmlResponse response)
        {
            PaymentResponseXmlAdapter adapter = new PaymentResponseXmlAdapter();
            PaymentResponse result = adapter.read(response);
            return result;
        }

        public override Task<PaymentResponse> Send(GatewayContext gatewayContext)
        {
            if (SessionContext == null)
            {
                throw new ArgumentException("SessionContext not set! Needs to be set, or use Send(GatewayContext, CessionContext) - you need to pass session context from initial request");
            }
            return Send(gatewayContext, SessionContext);
        }

    }
}
