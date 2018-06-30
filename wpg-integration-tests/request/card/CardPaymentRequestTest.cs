using System;
using System.Threading.Tasks;
using Worldpay;
using Xunit;

namespace wpgintegrationtests
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
            Assert.Equal("C", cvcResult.Description);

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

        [Fact]
        public void send_withoutCvc()
        {
            OrderDetails orderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            Shopper shopper = new Shopper("test@test.com", "123.123.123.123", new ShopperBrowser("text/html", "Mozilla/5.0 Chrome/62.0.3202.94 Safari/537.36"));

            CardDetails cardDetails = new CardDetails("4444333322221111", 1L, 2020L, "Cardholder name");

            CardPaymentRequest request = new CardPaymentRequest();
            request.OrderDetails = orderDetails;
            request.CardDetails = cardDetails;
            request.Shopper = shopper;

            Task<PaymentResponse> response = request.Send(GATEWAY_CONTEXT);
            PaymentResponse paymentResponse = response.Result;

            // check cvc status
            Payment payment = paymentResponse.Payment;
            Assert.NotNull(payment);

            CvcResult cvcResult = payment.CvcResult;
            Assert.NotNull(cvcResult);
            Assert.Equal("B", cvcResult.Description);
        }

        [Fact]
        public void send_withAddresses()
        {
            OrderDetails orderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            Shopper shopper = new Shopper("test@test.com", "123.123.123.123", new ShopperBrowser("text/html", "Mozilla/5.0 Chrome/62.0.3202.94 Safari/537.36"));

            CardDetails cardDetails = new CardDetails("4444333322221111", 1L, 2020L, "Cardholder name");

            Address billingAddress = new Address("123 test address", "blah", "1234", "GB");
            Address shippingAddress = new Address("987 test address", "blah", "4321", "GB");

            CardPaymentRequest request = new CardPaymentRequest();
            request.OrderDetails = orderDetails;
            request.CardDetails = cardDetails;
            request.Shopper = shopper;
            request.BillingAddress = billingAddress;
            request.ShippingAddress = shippingAddress;

            Task<PaymentResponse> response = request.Send(GATEWAY_CONTEXT);
            PaymentResponse paymentResponse = response.Result;

            // check payment present
            Payment payment = paymentResponse.Payment;
            Assert.NotNull(payment);
        }

        [Fact]
        public void send_createShopperToken_shopperFullDetails()
        {
            OrderDetails orderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            Shopper shopper = new Shopper("test@test.com", "123.123.123.123", new ShopperBrowser("text/html", "Mozilla/5.0 Chrome/62.0.3202.94 Safari/537.36"), "shopper123");

            CardDetails cardDetails = new CardDetails("4444333322221111", 1L, 2020L, "Cardholder name");
            cardDetails.CardHolderAddress = new Address("test", "test", "123 test street", "testridge", null, "test123", "testridge", null, "GB", "01234567890");

            TokenScope tokenScope = TokenScope.SHOPPER;
            string eventReference = "EVENT123";
            string eventReason = "event reason";
            DateTime expiry = DateTime.Now.AddDays(1);

            CreateTokenDetails createTokenDetails = new CreateTokenDetails(tokenScope, eventReference, eventReason, expiry);

            CardPaymentRequest request = new CardPaymentRequest();
            request.OrderDetails = orderDetails;
            request.CardDetails = cardDetails;
            request.Shopper = shopper;
            request.CreateTokenDetails = createTokenDetails;

            Task<PaymentResponse> response = request.Send(GATEWAY_CONTEXT);
            PaymentResponse paymentResponse = response.Result;

            // check payment present
            Payment payment = paymentResponse.Payment;
            Assert.NotNull(payment);

            // check token present
            Token token = payment.Token;
            Assert.NotNull(token);
            Assert.Equal("shopper123", token.ShopperId);
            Assert.Equal(tokenScope, token.Scope);

            // check token details
            TokenDetails tokenDetails = token.Details;
            Assert.NotNull(tokenDetails.PaymentTokenId);
            Assert.Equal(eventReference, tokenDetails.EventReference);
            Assert.Equal(eventReason, tokenDetails.EventReason);

            // check token expiry is within an hour of what we specified
            DateTime tokenExpiryResponse = tokenDetails.TokenExpiry;
            TimeSpan timeSpan = expiry.Subtract(tokenExpiryResponse);
            Assert.True(timeSpan.Seconds < 24*60*60, "Token expiry should not be greater than 244hrs different to requested expiry - expiry wanted: " + expiry + ", response: " + tokenExpiryResponse);

            // check token instrument
            Assert.NotNull(token.Instrument);
            Assert.IsType(typeof(TokenCardDetails), token.Instrument);

            TokenCardDetails tokenCardDetails = (TokenCardDetails)token.Instrument;
            Assert.Equal("VISA", tokenCardDetails.CardBrand);
            Assert.Equal("VISA_CREDIT", tokenCardDetails.CardBrand);
            Assert.Equal("N/A", tokenCardDetails.IssuerCountryCode);
            Assert.Equal("4444********1111", tokenCardDetails.ObfuscatedCardNumber);

            CardDetailsResult responseDetails = tokenCardDetails.CardDetailsResult;
            Assert.Null(responseDetails.HashedCardNumber);
            Assert.Equal("4444********1111", responseDetails.MaskedCardNumber);
            Assert.Equal(cardDetails.ExpiryMonth, responseDetails.ExpiryMonth);
            Assert.Equal(cardDetails.ExpiryYear, responseDetails.ExpiryYear);
            Assert.Equal(cardDetails.CardHolderName, responseDetails.CardHolderName);
        }

    }
}
