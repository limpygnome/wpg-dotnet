using System;
using System.Threading.Tasks;
using wpg.connection;
using wpg.connection.auth;
using wpg.domain;
using wpg.domain.card;
using wpg.domain.payment;
using wpg.domain.payment.result;
using wpg.exception;
using wpg.request.card;
using Xunit;

namespace wpgintegrationtests.request.card
{
    public class CardPaymentRequestTest : BaseIntegrationTest
    {

        [Fact]
        public void send_noAuth()
        {
            try
            {
                // given
                GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, new UserPassAuth("fake", "fake", "fake", "1234"));

                OrderDetails orderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
                Shopper shopper = new Shopper("test@test.com", "123.123.123.123", new ShopperBrowser("text/html", "Mozilla/5.0 Chrome/62.0.3202.94 Safari/537.36"));

                CardDetails cardDetails = new CardDetails("4444333322221111", 1L, 2020L, "Cardholder name", "123");

                // when
                CardPaymentRequest request = new CardPaymentRequest();
                request.OrderDetails = orderDetails;
                request.CardDetails = cardDetails;
                request.Shopper = shopper;
                Task<PaymentResponse> response = request.Send(gatewayContext);
                PaymentResponse result = response.Result;

                Assert.True(false, "Exception should have been thrown");
            }
            catch (AggregateException e)
            {
                var inner = e.InnerException;

                // then
                string expected = "Failed to make request - message from gateway: This request requires HTTP authentication.";
                Assert.Equal(expected, inner.Message);
                Assert.IsType(typeof(WpgRequestException), inner);
            }
        }

        [Fact]
        public void send()
        {
            OrderDetails orderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            Shopper shopper = new Shopper("test@test.com", "123.123.123.123", new ShopperBrowser("text/html", "Mozilla/5.0 Chrome/62.0.3202.94 Safari/537.36"));

            CardDetails cardDetails = new CardDetails("4444333322221111", 1L, 2020L, "Cardholder name", "123");

            CardPaymentRequest request = new CardPaymentRequest();
            request.OrderDetails = orderDetails;
            request.CardDetails = cardDetails;
            request.Shopper = shopper;

            Task<PaymentResponse> response = request.Send(GATEWAY_CONTEXT);
            PaymentResponse paymentResponse = response.Result;

            // check threeds missing
            Assert.Equal(PaymentStatus.PAYMENT_RESULT, paymentResponse.Status);
            Assert.Null(paymentResponse.ThreeDsDetails);

            // check payment
            Payment payment = paymentResponse.Payment;
            Assert.NotNull(payment);
            Amount expectedAmount = new Amount("GBP", 2, 1234L, DebitCreditIndicator.CREDIT);
            Assert.Equal(expectedAmount, payment.Amount);
            Assert.Equal(LastEvent.AUTHORISED, payment.LastEvent);

            // check payment method
            PaymentMethodType? paymentMethodType = payment.PaymentMethodType;
            Assert.Equal(PaymentMethodType.VISA, paymentMethodType);
            Assert.Equal("VISA_CREDIT-SSL", payment.PaymentMethodMask);

            // check balance
            Balance balance = payment.Balance;
            Assert.Equal(expectedAmount, balance.Amount);
            Assert.Equal("IN_PROCESS_AUTHORISED", balance.AccountType);

            // check results expected
            CvcResult cvcResult = payment.CvcResult;
            Assert.NotNull(cvcResult);
            Assert.Equal("NOT SENT TO ACQUIRER", cvcResult.Description);

            // check risk
            RiskScoreResult riskScoreResult = payment.RiskScoreResult;
            Assert.NotNull(riskScoreResult);
            Assert.Equal(1, riskScoreResult.Value);

            // check results not expected
            Assert.Null(payment.ISO8583Result);
            Assert.Null(payment.ThreeDSecureResult);
            Assert.Null(payment.AvsResult);
            Assert.Null(payment.AvvResult);
            Assert.Null(payment.Token);
        }

    }
}
