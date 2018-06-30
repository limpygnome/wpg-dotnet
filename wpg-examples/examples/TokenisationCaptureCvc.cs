using System;
using Worldpay;

namespace wpgexamples
{
    public class TokenisationCaptureCvc : DemoApp
    {
        public void Run(string xmlUser, string xmlPass, string merchantCode)
        {
            Auth auth = new UserPassAuth(xmlUser, xmlPass, merchantCode);
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper("email@email.com", "1.2.3.4", browser, "shopper123");

            OrderDetails orderDetails = new OrderDetails("test", new Amount("EUR", 2L, 1234L));
            Address address = new Address("address 1", "city", "post code", "GB");

            TokenisationPaymentResponse response = new TokenPaymentRequest("payment token id", TokenScope.SHOPPER, orderDetails, shopper, address, address, true)
                .Send(gatewayContext)
                .Result;

            Console.WriteLine(response.CaptureCvcUrl.Url);
        }
    }
}
