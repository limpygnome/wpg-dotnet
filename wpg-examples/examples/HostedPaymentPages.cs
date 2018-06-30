using System;
using Worldpay;

namespace wpgexamples
{
    public class HostedPaymentPages : DemoApp
    {
        public void Run(string xmlUser, string xmlPass, string merchantCode)
        {
            Auth auth = new UserPassAuth(xmlUser, xmlPass, merchantCode);
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            Address address = new Address("test road", "Cambridge", "CB0123", "GB");

            HostedPaymentPagesRequest request = new HostedPaymentPagesRequest();
            request.OrderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            request.BillingAddress = address;
            request.ShippingAddress = address;

            RedirectUrl response = request.Send(gatewayContext).Result;

            Console.WriteLine("Url: " + response.Url);
        }
    }
}
