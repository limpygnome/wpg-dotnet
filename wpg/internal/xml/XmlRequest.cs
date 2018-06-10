using System;
using System.Threading.Tasks;
using wpg.connection;
using wpg.@internal.xml.adapter;

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
            XmlClient client = new XmlClient();
            XmlResponse response = await client.send(buildParams);

            // Check for errors
            ErrorCodeAdapter.throwIfPresent(response);

            // Translate/adapt
            T result = Adapt(response);
            return result;
        }

        protected abstract void Validate(XmlBuildParams buildParams);

        protected abstract void Build(XmlBuildParams buildParams);

        protected abstract T Adapt(XmlResponse response);

        protected XmlEndpoint GetEndpoint()
        {
            return XmlEndpoint.PAYMENTS;
        }

    }
}
