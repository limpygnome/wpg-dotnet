using Worldpay;
using Xunit;

namespace wpgintegrationtests
{
    public class OrderInquiryRequestTest : BaseIntegrationTest
    {

        [Fact]
        public void inquiry_asExpected()
        {
            // Given

            OrderDetails orderDetails = new OrderDetails("threeds test order", new Amount("GBP", 2L, 1000L));

            CardDetails cardDetails = new CardDetails("4444333322221129", 1L, 2030L, "test");
            Shopper shopper = new Shopper("test@test.com", "123.123.123.123", new ShopperBrowser("accepts", "user agent"));
            CardPaymentRequest request = new CardPaymentRequest(orderDetails, cardDetails, shopper);
            PaymentResponse response = request.Send(GATEWAY_CONTEXT).Result;
            Assert.Equal(PaymentStatus.PAYMENT_RESULT, response.Status);

            // -- Due to replication delay, poll until auth
            pollUntil(orderDetails, LastEvent.AUTHORISED);

            // When
            Payment payment = new OrderInquiryRequest(orderDetails).Send(GATEWAY_CONTEXT).Result;

            // Then
            Assert.NotNull(payment);
            Assert.Equal(LastEvent.AUTHORISED, payment.LastEvent);
            Assert.NotNull(payment.CardDetailsResult);
        }

    }
}
