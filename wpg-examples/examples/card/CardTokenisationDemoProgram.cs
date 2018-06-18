using System;
using System.Threading.Tasks;
using wpg.connection;
using wpg.connection.auth;
using wpg.domain;
using wpg.domain.card;
using wpg.domain.payment;
using wpg.domain.tokenisation;
using wpg.request.card;

namespace wpgexamples
{
    public class CardTokenisationDemoProgram : DemoApp
    {
        public void Run(string xmlUser, string xmlPass, string merchantCode)
        {
            Auth auth = new UserPassAuth(xmlUser, xmlPass, merchantCode);
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            Address address = new Address("test road", "Cambridge", "CB0123", "GB");

            CardPaymentRequest request = new CardPaymentRequest();
            request.OrderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            request.CardDetails = new CardDetails("4444333322221129", 1, 2020, "John Doe");
            request.BillingAddress = address;
            request.ShippingAddress = address;

            // Just add Shopper ID and CreateTokenDetails for tokenisation
            request.Shopper = new Shopper(null, "shopperId123");
            request.CreateTokenDetails = new CreateTokenDetails("TOKEN_EVENT_123", "monthly subscription");

            Task<PaymentResponse> asyncResponse = request.Send(gatewayContext);
            PaymentResponse response = asyncResponse.Result;

            Console.WriteLine(response);
        }
    }
}
