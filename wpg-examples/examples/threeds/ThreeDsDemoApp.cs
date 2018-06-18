using System.Threading.Tasks;
using wpg.connection;
using wpg.connection.auth;
using wpg.domain.payment;
using wpg.request.threeds;

namespace wpgexamples
{
    public class ThreeDsDemoApp : DemoApp
    {
        public void Run(string xmlUser, string xmlPass, string merchantCode)
        {
            Auth auth = new UserPassAuth(xmlUser, xmlPass, merchantCode);
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            SubmitThreeDSRequest request = new SubmitThreeDSRequest();
            request.OrderCode = "order code";
            request.PaResponse = "pa response from issuer";

            SessionContext existingSessionContext = GetSessionContextInSession();
            Task<PaymentResponse> asyncResponse = request.Send(gatewayContext, existingSessionContext);
            PaymentResponse response = asyncResponse.Result;
        }

        private SessionContext GetSessionContextInSession()
        {
            // Retrieve this from the shopper's session, it should be the same session context from their
            // initial card payment. You can retrieve this from the ThreeDsDetails returned in the response from
            // the initial card payment
            return new SessionContext();
        }
    }
}
