using System.Collections.Generic;
using wpg.domain;
using wpg.domain.card;
using wpg.domain.payment;
using wpg.domain.tokenisation;
using wpg.exception;
using wpg.request.card;
using wpg.request.tokenisation;
using Xunit;

namespace wpgintegrationtests.request.tokenisation
{
    public class TokenisationTest : BaseIntegrationTest
    {

        // Maximum times to poll a token to check whether it has been deleted.
        private const int MAX_ATTEMPTS_POLL_TOKEN_DELETION = 10;

        // Delay between polls, for when checking a token has been deletedd, in milliseconds.
        private const int POLL_TOKEN_DELETION_DELAY = 2000;

        [Fact]
        public void captureCvc()
        {
            // Given
            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper("email@email.com", "1.2.3.4", browser, "shopper123");
            Token token = setupOrder(new CreateTokenDetails(TokenScope.SHOPPER, "event_ref", "reason"), shopper);

            OrderDetails orderDetails = new OrderDetails("test", new Amount("EUR", 2L, 1234L));
            Address address = new Address("address 1", "city", "post code", "GB");

            // When
            TokenisationPaymentResponse response = new TokenPaymentRequest(token.PaymentTokenId, TokenScope.SHOPPER, orderDetails, shopper, address, address, true)
                .Send(GATEWAY_CONTEXT)
                .Result;

            // Then
            Assert.NotNull(response);
            Assert.Null(response.PaymentResponse);
            Assert.NotNull(response.CaptureCvcUrl);
            Assert.NotNull(response.CaptureCvcUrl.Url);
        }

        [Fact]
        public void captureCvc_simple()
        {
            // Given
            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper(null, null, browser, "shopper123");
            Token token = setupOrder(new CreateTokenDetails(TokenScope.SHOPPER, "event_ref", "reason"), shopper);

            OrderDetails orderDetails = new OrderDetails("test", new Amount("EUR", 2L, 1234L));

            // When
            TokenisationPaymentResponse response = new TokenPaymentRequest(
                token.PaymentTokenId, TokenScope.SHOPPER, orderDetails, shopper, true)
                    .Send(GATEWAY_CONTEXT)
                    .Result;

            // Then
            Assert.NotNull(response);
            Assert.Null(response.PaymentResponse);
            Assert.NotNull(response.CaptureCvcUrl);
            Assert.NotNull(response.CaptureCvcUrl.Url);
        }

        [Fact]
        public void makePayment()
        {
            // Given
            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper(null, null, browser, "shopper123");
            Token token = setupOrder(new CreateTokenDetails(TokenScope.SHOPPER, "event_ref", "reason"), shopper);

            OrderDetails orderDetails = new OrderDetails("test", new Amount("EUR", 2L, 1234L));
            Address address = new Address("address 1", "city", "post code", "GB");

            // When
            TokenisationPaymentResponse response = new TokenPaymentRequest(
                    token.PaymentTokenId, TokenScope.SHOPPER, orderDetails, shopper, address, address, false)
                    .Send(GATEWAY_CONTEXT)
                    .Result;

            // Then
            Assert.NotNull(response);
            Assert.NotNull(response.PaymentResponse);
            Assert.Equal(PaymentStatus.PAYMENT_RESULT, response.PaymentResponse.Status);

            Payment payment = response.PaymentResponse.Payment;
            Assert.NotNull(payment);
            Assert.Equal(PaymentMethodType.VISA, payment.PaymentMethodType);
            Assert.Equal(LastEvent.AUTHORISED, payment.LastEvent);
        }

        [Fact]
        public void makePayment_overrideCardDetails()
        {
            // Given
            Address cardAddress = new Address("override address 1", "override city", "override post code", "FR");
            CardDetails cardDetails = new CardDetails("54444444444447", 12L, 2099L, "mr override", "123", cardAddress);

            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper(null, null, browser, "shopper123");
            Token token = setupOrder(new CreateTokenDetails(TokenScope.SHOPPER, "event_ref", "reason"), shopper);

            OrderDetails orderDetails = new OrderDetails("test", new Amount("EUR", 2L, 1234L));
            Address address = new Address("address 1", "city", "post code", "GB");

            // When
            TokenPaymentRequest request = new TokenPaymentRequest(
                token.PaymentTokenId, TokenScope.SHOPPER, orderDetails, shopper, address, address, false);
            request.CardDetails = cardDetails;

            TokenisationPaymentResponse response = request
                .Send(GATEWAY_CONTEXT)
                .Result;

            // Then
            Assert.NotNull(response);
            Assert.NotNull(response.PaymentResponse);
            Assert.Equal(PaymentStatus.PAYMENT_RESULT, response.PaymentResponse.Status);

            Payment payment = response.PaymentResponse.Payment;
            Assert.NotNull(payment);
            Assert.Equal(PaymentMethodType.VISA, payment.PaymentMethodType);
            Assert.Equal(LastEvent.AUTHORISED, payment.LastEvent);

            // TODO assert details change; currently test merchant not coming back with details
        }

        [Fact]
        public void makePayment_simple()
        {
            // Given
            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper(null, null, browser, "shopper123");
            Token token = setupOrder(new CreateTokenDetails(TokenScope.SHOPPER, "event_ref", "reason"), shopper);

            OrderDetails orderDetails = new OrderDetails("test", new Amount("EUR", 2L, 1234L));

            // When
            TokenisationPaymentResponse response = new TokenPaymentRequest(token.PaymentTokenId, orderDetails, shopper)
                .Send(GATEWAY_CONTEXT)
                .Result;

            // Then
            Assert.NotNull(response);
            Assert.NotNull(response.PaymentResponse);
            Assert.Equal(PaymentStatus.PAYMENT_RESULT, response.PaymentResponse.Status);

            Payment payment = response.PaymentResponse.Payment;
            Assert.NotNull(payment);
            Assert.Equal(PaymentMethodType.VISA, payment.PaymentMethodType);
            Assert.Equal(LastEvent.AUTHORISED, payment.LastEvent);
        }

        [Fact]
        public void endToEnd_createShopperToken()
        {
            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper("email@email.com", "1.2.3.4", browser, "shopper123");

            // Create token
            Token token = setupOrder(new CreateTokenDetails(TokenScope.SHOPPER, "event_ref", "reason"), shopper);

            // Fetch token
            Token fetchedToken = new FetchTokenRequest(token.PaymentTokenId, shopper.ShopperId)
                .Send(GATEWAY_CONTEXT)
                .Result;

            Assert.NotNull(fetchedToken);
            Assert.Equal(token.PaymentTokenId, fetchedToken.PaymentTokenId);
            Assert.Equal(token.Scope, fetchedToken.Scope);
            Assert.Equal(token.Instrument, fetchedToken.Instrument);
            Assert.Equal(token.ShopperId, fetchedToken.ShopperId);
            Assert.Equal(token.Details.PaymentTokenId, fetchedToken.Details.PaymentTokenId);
            Assert.Equal(token.Details.EventReason, fetchedToken.Details.EventReason);
            Assert.Equal(token.Details.EventReference, fetchedToken.Details.EventReference);
            Assert.Equal(token.Details.TokenExpiry, fetchedToken.Details.TokenExpiry);

            // -- Token event wont be set when fetched, as token not being created/used/matched
            Assert.NotNull(token.Details.TokenEvent);
            Assert.Null(fetchedToken.Details.TokenEvent);

            // Fetch by shopper ID
            List<Token> shopperTokens = new FetchTokensByShopperRequest(shopper.ShopperId)
                .Send(GATEWAY_CONTEXT)
                .Result;

            Assert.NotNull(fetchedToken);
            Assert.Equal(1, shopperTokens.Count);
            Assert.Equal(fetchedToken, shopperTokens[0]);

            // Delete token
            Void v = new DeleteTokenRequest(token.PaymentTokenId, shopper.ShopperId)
                .Send(GATEWAY_CONTEXT)
                .Result;

            // Check token is deleted
            pollForDeletion(token);
        }

        [Fact]
        public void endToEnd_createMerchantToken()
        {
            ShopperBrowser browser = new ShopperBrowser("accepts", "user agent");
            Shopper shopper = new Shopper(browser);

            // Create token
            Token token = setupOrder(new CreateTokenDetails(TokenScope.MERCHANT, "event_ref", "reason"), shopper);

                // Fetch token
            Token fetchedToken = new FetchTokenRequest(token.PaymentTokenId)
                .Send(GATEWAY_CONTEXT)
                .Result;

            Assert.NotNull(fetchedToken);
            Assert.Equal(token.PaymentTokenId, fetchedToken.PaymentTokenId);
            Assert.Equal(token.Scope, fetchedToken.Scope);
            Assert.Equal(token.Instrument, fetchedToken.Instrument);
            Assert.Equal(token.ShopperId, fetchedToken.ShopperId);
            Assert.Equal(token.Details.PaymentTokenId, fetchedToken.Details.PaymentTokenId);
            Assert.Equal(token.Details.EventReason, fetchedToken.Details.EventReason);
            Assert.Equal(token.Details.EventReference, fetchedToken.Details.EventReference);
            Assert.Equal(token.Details.TokenExpiry, fetchedToken.Details.TokenExpiry);

            // -- Token event wont be set when fetched, as token not being created/used/matched
            Assert.NotNull(token.Details.TokenEvent);
            Assert.Null(fetchedToken.Details.TokenEvent);

            // Delete token
            new DeleteTokenRequest(token.PaymentTokenId)
                .Send(GATEWAY_CONTEXT);

            // Check token is deleted
            pollForDeletion(token);
        }

        private Token setupOrder(CreateTokenDetails createTokenDetails, Shopper shopper)
        {
            OrderDetails orderDetails = new OrderDetails("threeds test order", new Amount("GBP", 2L, 1000L));
            CardDetails cardDetails = new CardDetails("4444333322221129", 1L, 2030L, "test");
            CardPaymentRequest request = new CardPaymentRequest(orderDetails, cardDetails, shopper);
            request.CreateTokenDetails = createTokenDetails;

            PaymentResponse response = request.Send(GATEWAY_CONTEXT).Result;
            Assert.Equal(PaymentStatus.PAYMENT_RESULT, response.Status);

            Token token = response.Payment.Token;
            Assert.NotNull(token);

            // Wait for order to be replicated
            pollUntil(orderDetails, LastEvent.AUTHORISED);

            return token;
        }

        // Required due to replication delay
        private void pollForDeletion(Token token)
        {
            int attempts = 0;
            bool success = false;

            do
            {
                // Attempt to poll...
                try
                {
                    new FetchTokenRequest(token.PaymentTokenId)
                        .Send(GATEWAY_CONTEXT);
                }
                catch (WpgErrorResponseException e)
                {
                    Assert.Equal("Token does not exist", e.GatewayErrorMessage);
                    Assert.Equal(5L, e.GatewayErrorCode);
                    success = true;
                }

                // Sleep if unsuccessful
                if (!success)
                {
                    System.Threading.Thread.Sleep(POLL_TOKEN_DELETION_DELAY);
                }

            }
            while (!success && attempts++ < MAX_ATTEMPTS_POLL_TOKEN_DELETION);

            if (!success)
            {
                Assert.True(false, "Exception should have been thrown, stating token no longer exists");
            }
        }

    }
}
