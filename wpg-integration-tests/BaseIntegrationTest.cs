using System;
using System.Net.Http;
using wpg.connection;
using wpg.connection.auth;
using Xunit;

namespace wpgintegrationtests
{
    public abstract class BaseIntegrationTest
    {
        protected static readonly GatewayContext GATEWAY_CONTEXT;
        protected static readonly HttpClient client = new HttpClient();

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

    }
}
