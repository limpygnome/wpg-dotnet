using System;
using Worldpay;

namespace wpgexamples
{
    public class PayPal : DemoApp
    {
        public void Run(string xmlUser, string xmlPass, string merchantCode)
        {
            Auth auth = new UserPassAuth(xmlUser, xmlPass, merchantCode);
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            PayPalPaymentRequest request = new PayPalPaymentRequest();
            request.OrderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            request.Shopper = new Shopper("shopper@worldpay.com");
            request.SetResultURLs("https://test.worldpay.com");

            RedirectUrl response = request.Send(gatewayContext).Result;

            Console.WriteLine("Url: " + response.Url);
        }
    }
}
