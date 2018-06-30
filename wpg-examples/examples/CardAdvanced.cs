using System;
using System.Threading.Tasks;
using Worldpay;

namespace wpgexamples
{
    public class CardAdvanced : DemoApp
    {
        public void Run(string xmlUser, string xmlPass, string merchantCode)
        {
            Auth auth = new UserPassAuth(xmlUser, xmlPass, merchantCode);
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            try
            {
                Address address = new Address("test road", "Cambridge", "CB0123", "GB");

                CardPaymentRequest request = new CardPaymentRequest();
                request.OrderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
                request.CardDetails = new CardDetails("4444333322221129", 1, 2020, "John Doe");
                request.BillingAddress = address;
                request.ShippingAddress = address;

                ShopperBrowser browser = new ShopperBrowser("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                                                            "Mozilla/5.0 (Macintosh; Intel Mac OS X) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36");
                request.Shopper = new Shopper("shopper@worldpay.com", "123.123.123.123", browser);

                Task<PaymentResponse> asyncResponse = request.Send(gatewayContext);
                PaymentResponse response = asyncResponse.Result;

                Console.WriteLine(response);
            }
            catch (AggregateException e)
            {
                if (e.InnerException is WpgException)
                {
                    Console.WriteLine("Failed (WPG): " + e.InnerException.Message);
                }
                else
                {
                    Console.WriteLine("Failed: " + e.InnerException.Message);
                }
            }
        }
    }
}
