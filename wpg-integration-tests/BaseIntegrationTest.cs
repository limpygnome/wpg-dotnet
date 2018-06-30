using System;
using System.Net.Http;
using Worldpay;
using Xunit;

namespace wpgintegrationtests
{
    public abstract class BaseIntegrationTest
    {
        protected static readonly GatewayContext GATEWAY_CONTEXT;
        protected static readonly HttpClient client = new HttpClient();

        // Maximum number of times to poll an order for its status / last event to change.
        // This is only required due to replication or/and processing delay.
        protected const int ORDER_INQUIRY_ATTEMPTS = 40;

         // Milliseconds delay between order inquiry polling attempts.
        protected const int ORDER_INQUIRY_DELAY = 2500;

        static BaseIntegrationTest()
        {
            string user = Environment.GetEnvironmentVariable("sdk.user");
            string pass = Environment.GetEnvironmentVariable("sdk.pass");
            string merchantCode = Environment.GetEnvironmentVariable("sdk.merchantCode");
            string installationId = Environment.GetEnvironmentVariable("sdk.installationId");

            if (user == null || pass == null || merchantCode == null || installationId == null)
            {
                throw new ArgumentException("Tests ran without credentials specified");
            }

            GATEWAY_CONTEXT = new GatewayContext(GatewayEnvironment.SANDBOX, new UserPassAuth(user, pass, merchantCode, installationId));
        }

        protected void assertStatusCode(string url, int expectedStatusCode)
        {
            var response = client.GetAsync(url).Result;
            int statusCode = (int) response.StatusCode;
            Assert.True(statusCode == expectedStatusCode, "Failed request assertion - expectedStatusCode=" + expectedStatusCode + ", statusCode=" + statusCode + ", url=" + url);
        }

        protected LastEvent? pollUntil(OrderDetails orderDetails, LastEvent expectedLastEvent)
        {
            bool ready = false;
            int attempts = 0;
            LastEvent? result = null;

            do
            {
                try
                {
                    Payment payment = new OrderInquiryRequest(orderDetails)
                        .Send(GATEWAY_CONTEXT)
                        .Result;
                    result = payment.LastEvent;

                    if (expectedLastEvent == result)
                    {
                        ready = true;
                    }
    }
                catch (AggregateException)
                {
                }

                // Sleep until ready, probably replication delay...
                if (!ready)
                {
                    System.Threading.Thread.Sleep(ORDER_INQUIRY_DELAY);
                }
            }
            while (!ready && attempts++ < ORDER_INQUIRY_ATTEMPTS);

            if (result == null)
            {
                throw new InvalidOperationException("Order not ready - unable to inquire status");
            }
            else if (result != expectedLastEvent)
            {
                throw new InvalidOperationException("Order does not have expected last event - currently: " + result + ", expected: " + expectedLastEvent);
            }

            return result;
        }


    }
}
