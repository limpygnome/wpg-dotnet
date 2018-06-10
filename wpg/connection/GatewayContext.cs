using System;
using wpg.connection.auth;
namespace wpg.connection
{
    public class GatewayContext
    {
        public GatewayContext(GatewayEnvironment environment, IAuth auth)
        {
            this.Environment = environment;
            this.Auth = auth;
        }

        public GatewayEnvironment Environment { get; set;  }

        public IAuth Auth { get; set; }

    }
}
