using System;
using System.Threading.Tasks;
using wpg.connection;
using wpg.connection.auth;
using wpg.domain;
using wpg.domain.card;
using wpg.domain.payment;
using wpg.request.card;

namespace wpgexamples
{
    public class CardDemoProgram : DemoApp
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

            Task<PaymentResponse> asyncResponse = request.Send(gatewayContext);
            PaymentResponse response = asyncResponse.Result;

            Console.WriteLine(response);
        }
    }
}
