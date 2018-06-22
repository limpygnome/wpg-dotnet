using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using wpg.connection;
using wpg.domain;
using wpg.domain.card;
using wpg.domain.payment;
using wpg.exception;
using wpg.request.card;
using wpg.request.threeds;
using Xunit;

namespace wpgintegrationtests.request.threeds
{
    public class SubmitThreeDSRequestTest : BaseIntegrationTest
    {
        private SessionContext sessionContext;
        private OrderDetails orderDetails;
        private PaymentResponse initialPaymentResponse;

        public SubmitThreeDSRequestTest()
        {
            sessionContext = new SessionContext();
            orderDetails = new OrderDetails("threeds test order", new Amount("GBP", 2L, 1000L));

            // Send initial card payment
            CardDetails cardDetails = new CardDetails("4444333322221129", 1L, 2030L, "3D");
            Shopper shopper = new Shopper("test@test.com", "123.123.123.123", new ShopperBrowser("accepts", "user agent"));
            CardPaymentRequest initialRequest = new CardPaymentRequest(orderDetails, cardDetails, shopper);
            initialPaymentResponse = initialRequest.Send(GATEWAY_CONTEXT, sessionContext).Result;

            // Check threeds authentication is required
            Assert.NotNull(initialPaymentResponse.ThreeDsDetails);
        }

        [Fact]
        public void sendRubbish()
        {
            try
            {
                // When
                SubmitThreeDSRequest request = new SubmitThreeDSRequest(orderDetails, "rubbish pa response");
                request.Send(GATEWAY_CONTEXT, sessionContext);
            }
            catch (WpgErrorResponseException e)
            {
                // Then
                Assert.Equal(e.GatewayErrorCode, 7L);
                Assert.Equal(e.GatewayErrorMessage, "verification of PaRes failed");
            }
        }

        [Fact]
        public void sendLegit()
        {
            // Given
            string cookie = getInitialIssuerPage();
            string paResponse = getPaResponseFromIssuerPage(cookie);

            // When
            PaymentResponse paymentResponse = new SubmitThreeDSRequest(orderDetails, paResponse)
                .Send(GATEWAY_CONTEXT, sessionContext).Result;

            // Then
            Assert.Null(paymentResponse.ThreeDsDetails);
            Assert.NotNull(paymentResponse.Payment);
            Assert.Equal(paymentResponse.Payment.LastEvent, LastEvent.AUTHORISED);
        }

        private string getInitialIssuerPage()
        {
            // Build request for 3ds simulator
            string issuerUrl = initialPaymentResponse.ThreeDsDetails.IssuerURL;

            string paReq = HttpUtility.UrlEncode(initialPaymentResponse.ThreeDsDetails.PaRequest);
            string postData = "PaReq=" + paReq + "&TermUrl=http://localhost:3000&MD=";
            HttpContent content = new StringContent(postData);

            // Make request...
            var responseMessage = client.PostAsync(issuerUrl, content).Result;

            // Check response status code
            int statusCode = (int)responseMessage.StatusCode;
            Assert.Equal(200, statusCode);

            // Extract cookie (session)
            var cookieHeaders = responseMessage.Headers.GetValues("Set-Cookie");
            StringBuilder cookieValue = new StringBuilder();
            foreach (var cookieHeader in cookieHeaders)
            {
                cookieValue.Append(cookieHeader).Append(";");
            }
            return cookieValue.ToString();
        }

        private string getPaResponseFromIssuerPage(string cookie)
        {
            // Build request for form submission (second request) in simulator...
            string issuerSubmitPage = "https://secure-test.worldpay.com/servlet/GenerateThreeDSParesServlet";

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, issuerSubmitPage);
            httpRequest.Headers.Add("Cookie", cookie);
            string paReq = HttpUtility.UrlEncode(initialPaymentResponse.ThreeDsDetails.PaRequest);
            string postData = "paResMagicValues=IDENTIFIED&parequest=" + paReq + "&termUrl=http://localhost:3000&MD=";

            HttpContent content = new StringContent(postData);
            httpRequest.Content = content;

            // Send request
            var responseMessage = client.SendAsync(httpRequest).Result;

            // Check response status code
            int statusCode = (int)responseMessage.StatusCode;
            Assert.Equal(200, statusCode);


            // Extract PaRes (3ds issuer response)
            var response = responseMessage.Content.ToString();
            var matches = Regex.Match(response, "(?:.+)<input name=\"PaRes\" type=\"hidden\" value=\"([^\"]+)\"\\s+/>(?:.+)");

            Assert.True(matches.Success);

            string paRes = matches.Groups[1].Value;
            return paRes;
        }

    }
}
