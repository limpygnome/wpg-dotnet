using System.Threading.Tasks;
using Worldpay.@internal.xml.adapter;

namespace Worldpay.@internal.xml
{
    public abstract class XmlRequest<T>
    {

        public virtual Task<T> Send(GatewayContext gatewayContext)
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

        internal abstract void Validate(XmlBuildParams buildParams);

        internal abstract void Build(XmlBuildParams buildParams);

        internal abstract T Adapt(XmlResponse response);

        internal XmlEndpoint GetEndpoint()
        {
            return XmlEndpoint.PAYMENTS;
        }

    }
}
