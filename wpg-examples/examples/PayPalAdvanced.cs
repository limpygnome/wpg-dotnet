using System;
using System.Threading.Tasks;
using Worldpay;

namespace wpgexamples
{
    public class PayPalAdvanced : DemoApp
    {
        public void Run(string xmlUser, string xmlPass, string merchantCode)
        {
            Auth auth = new UserPassAuth(xmlUser, xmlPass, merchantCode);
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            try
            {
                Address address = new Address("test road", "Cambridge", "CB0123", "GB");

                PayPalPaymentRequest request = new PayPalPaymentRequest();
                request.OrderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
                request.Shopper = new Shopper("shopper@worldpay.com", "shopperId123");
                request.BillingAddress = address;
                request.ShippingAddress = address;
                request.LanguageCode = "fr";
                request.SuccessURL = "https://success.worldpay.com";
                request.FailureURL = "https://failure.worldpay.com";
                request.CancelURL = "https://cancel.worldpay.com";

                Task<RedirectUrl> asyncResponse = request.Send(gatewayContext);
                RedirectUrl response = asyncResponse.Result;

                Console.WriteLine("Url: " + response.Url);
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
