using Worldpay;
using Xunit;

namespace wpgintegrationtests
{
    public class HostedPaymentPagesRequestTest : BaseIntegrationTest
    {
        private RedirectUrl redirectUrl;

        [Fact]
        public void basicOrder()
        {
            // Given
            OrderDetails orderDetails = new OrderDetails("test order", new Amount("GBP", 2, 1234L));

            // When
            HostedPaymentPagesRequest request = new HostedPaymentPagesRequest(orderDetails);
            redirectUrl = request.Send(GATEWAY_CONTEXT).Result;

            // Then
            assertStatusCode(redirectUrl.Url, 200);
        }

        [Fact]
        public void orderUrl_vanilla()
        {
            givenOrder();
            assertStatusCode(redirectUrl.Url, 200);
        }

        [Fact]
        public void orderUrl_malformed()
        {
            givenOrder();
            assertStatusCode(redirectUrl.Url + "&blah=ttest", 400);
        }

        [Fact]
        public void orderUrl_withResultUrls()
        {
            givenOrder();

            PaymentPagesRedirectBuilder builder = redirectUrl.CreatePaymentPagesBuilder();
            builder.SuccessUrl = "https://success.worldpay.com";
            builder.CancelUrl = "https://cancel.worldpay.com";
            builder.ErrorUrl = "https://error.worldpay.com";
            builder.FailureUrl = "https://failure.worldpay.com";
            builder.PendingUrl = "https://pending.worldpay.com";

            string url = builder.Build();

            assertStatusCode(url, 200);
        }

        [Fact]
        public void orderUrl_withPreferredPaymentMethod()
        {
            givenOrder();

            PaymentPagesRedirectBuilder builder = redirectUrl.CreatePaymentPagesBuilder();
            builder.PreferredPaymentMethodType = PaymentMethodType.VISA;

            string url = builder.Build();

            assertStatusCode(url, 200);
        }

        [Fact]
        public void orderUrl_withCountryLanguage()
        {
            givenOrder();

            PaymentPagesRedirectBuilder builder = redirectUrl.CreatePaymentPagesBuilder();
            builder.Country = "gb";
            builder.Language = "en";

            string url = builder.Build();

            assertStatusCode(url, 200);
        }

        [Fact]
        public void orderUrl_withCountry2()
        {
            givenOrder();

            PaymentPagesRedirectBuilder builder = redirectUrl.CreatePaymentPagesBuilder();
            builder.Country = "GB";
            builder.Language = "en";

            string url = builder.Build();

            assertStatusCode(url, 200);
        }

        [Fact]
        public void orderUrl_withEverything()
        {
            givenOrder();

            PaymentPagesRedirectBuilder builder = redirectUrl.CreatePaymentPagesBuilder();
            builder.SuccessUrl = "https://success.worldpay.com";
            builder.CancelUrl = "https://cancel.worldpay.com";
            builder.ErrorUrl = "https://error.worldpay.com";
            builder.FailureUrl = "https://failure.worldpay.com";
            builder.PendingUrl = "https://pending.worldpay.com";
            builder.PreferredPaymentMethodType = PaymentMethodType.VISA;

            string url = builder.Build();

            assertStatusCode(url, 200);
        }

        private void givenOrder()
        {
            OrderDetails orderDetails = new OrderDetails("test order", new Amount("GBP", 2, 1234L));
            HostedPaymentPagesRequest request = new HostedPaymentPagesRequest(orderDetails, new Shopper("test@worldpay.com"));
            redirectUrl = request.Send(GATEWAY_CONTEXT).Result;
        }

    }
}
