using wpg.domain;
using wpg.domain.apm;
using wpg.domain.payment;
using wpg.domain.redirect;
using wpg.domain.tokenisation;
using wpg.request.apm;
using Xunit;

namespace wpgintegrationtests.request.apm
{
    public class PayPalPaymentRequestTest : BaseIntegrationTest
    {
        
        [Fact]
        public void orderUrl()
        {
            // Given
            OrderDetails orderDetails = new OrderDetails("description", new Amount("GBP", 2L, 1234L));
            Shopper shopper = new Shopper("test@worldpay.com");
            string resultURL = "https://result.worldpay.com";

            // When
            RedirectUrl redirectUrl = new PayPalPaymentRequest(orderDetails, shopper, resultURL)
                .Send(GATEWAY_CONTEXT)
                .Result;

            // Then
            assertStatusCode(redirectUrl.Url, 200);
        }

        [Fact]
        public void languageCode()
        {
            // Given
            OrderDetails orderDetails = new OrderDetails("description", new Amount("EUR", 2L, 1234L));
            Shopper shopper = new Shopper("test@worldpay.com");
            string resultURL = "https://result.worldpay.com";

            // When
            PayPalPaymentRequest request = new PayPalPaymentRequest(orderDetails, shopper, resultURL);
            request.SetLanguage(PayPalLanguage.CHINESE);
            RedirectUrl redirectUrl = request.Send(GATEWAY_CONTEXT).Result;

            // Then
            assertStatusCode(redirectUrl.Url, 200);
        }

        [Fact]
        public void resultUrl()
        {
            // Given
            OrderDetails orderDetails = new OrderDetails("description", new Amount("EUR", 2L, 1234L));
            Shopper shopper = new Shopper("test@worldpay.com");

            // When
            PayPalPaymentRequest request = new PayPalPaymentRequest();
            request.OrderDetails = orderDetails;
            request.Shopper = shopper;
            request.SetResultURLs("https://result.worldpay.com");

            RedirectUrl redirectUrl = request.Send(GATEWAY_CONTEXT).Result;

            // Then
            assertStatusCode(redirectUrl.Url, 200);
        }

        [Fact]
        public void resultUrls()
        {
            // Given
            OrderDetails orderDetails = new OrderDetails("description", new Amount("EUR", 2L, 1234L));
            Shopper shopper = new Shopper("test@worldpay.com");

            // When
            PayPalPaymentRequest request = new PayPalPaymentRequest();
            request.OrderDetails = orderDetails;
            request.Shopper = shopper;
            request.SuccessURL = "https://success.worldpay.com";
            request.FailureURL = "https://failure.worldpay.com";
            request.CancelURL = "https://cancel.worldpay.com";

            RedirectUrl redirectUrl = request.Send(GATEWAY_CONTEXT).Result;

            // Then
            assertStatusCode(redirectUrl.Url, 200);
        }

        [Fact]
        public void billingAndShippingAddress()
        {
            // Given
            OrderDetails orderDetails = new OrderDetails("description", new Amount("EUR", 2L, 1234L));
            Shopper shopper = new Shopper("test@worldpay.com");
            string resultURL = "https://result.worldpay.com";

            // When
            PayPalPaymentRequest request = new PayPalPaymentRequest();
            request.OrderDetails = orderDetails;
            request.Shopper = shopper;
            request.SetResultURLs(resultURL);
            request.BillingAddress = new Address("123 billing address", "billing", "1234", "GB");
            request.ShippingAddress = new Address("123 shipping address", "shipping", "1234", "GB");

            RedirectUrl redirectUrl = request.Send(GATEWAY_CONTEXT).Result;

            // Then
            assertStatusCode(redirectUrl.Url, 200);
        }

        [Fact]
        public void tokenise()
        {
            // Given
            OrderDetails orderDetails = new OrderDetails("description", new Amount("EUR", 2L, 1234L));
            Shopper shopper = new Shopper("test@worldpay.com", "shopperId");
            string resultURL = "https://result.worldpay.com";

            // When
            PayPalPaymentRequest request = new PayPalPaymentRequest();
            request.OrderDetails = orderDetails;
            request.Shopper = shopper;
            request.SetResultURLs(resultURL);
            request.CreateTokenDetails = new CreateTokenDetails("event_reference", "reason");

            RedirectUrl redirectUrl = request.Send(GATEWAY_CONTEXT).Result;

            // Then
            assertStatusCode(redirectUrl.Url, 200);
        }

    }
}
