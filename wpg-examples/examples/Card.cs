using System;
using Worldpay;

namespace wpgexamples
{
    public class Card : DemoApp
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

            PaymentResponse response = request.Send(gatewayContext).Result;

            Console.WriteLine(response);
        }
    }
}
