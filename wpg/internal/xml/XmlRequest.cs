using System;
using System.Threading.Tasks;
using wpg.connection;
namespace wpg.@internal.xml
{
    public abstract class XmlRequest<T>
    {

        public Task<T> Send(GatewayContext gatewayContext)
        {
            return Send(gatewayContext, new SessionContext());
        }

        public async Task<T> Send(GatewayContext gatewayContext, SessionContext sessionContext)
        {
            // Prepareπ
            XmlEndpoint endpoint = GetEndpoint();
            XmlBuilder builder = new XmlBuilder(endpoint);
            XmlBuildParams buildParams = new XmlBuildParams(gatewayContext, sessionContext, builder);

            // Validate and build
            Validate(buildParams);
            Build(buildParams);

            // Send
            String xml = buildParams.Builder.ToString();

            // Check for errors

            // Translate/adapt
            T result = Adapt();
            return result;
        }

        protected abstract void Validate(XmlBuildParams buildParams);

        protected abstract void Build(XmlBuildParams buildParams);

        protected abstract T Adapt();

        protected XmlEndpoint GetEndpoint()
        {
            return XmlEndpoint.PAYMENTS;
        }

    }
}
