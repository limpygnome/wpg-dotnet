using System;
using wpg.connection;
using wpg.connection.auth;

namespace wpgintegrationtests
{
    public class BaseIntegrationTest
    {
        protected static readonly GatewayContext GATEWAY_CONTEXT;

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

    }
}
