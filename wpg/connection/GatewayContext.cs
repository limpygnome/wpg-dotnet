namespace Worldpay
{
    public class GatewayContext
    {
        public GatewayContext(GatewayEnvironment environment, Auth auth)
        {
            this.Environment = environment;
            this.Auth = auth;
        }

        public GatewayEnvironment Environment { get; set;  }

        public Auth Auth { get; set; }

    }
}
