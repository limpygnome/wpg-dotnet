using System;
using wpg.connection;
namespace wpg.@internal.xml
{
    public class XmlBuildParams
    {
        public XmlBuildParams(GatewayContext gatewayContext, SessionContext sessionContext, XmlBuilder builder)
        {
            this.GatewayContext = gatewayContext;
            this.SessionContext = sessionContext;
            this.Builder = builder;
        }

        public GatewayContext GatewayContext { get; set; }
        public SessionContext SessionContext { get; set; }
        public XmlBuilder Builder { get; set; }
        public bool IsBatch { get; set; }

    }
}
