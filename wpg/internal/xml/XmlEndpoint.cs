using System;
using wpg.connection;

namespace wpg.@internal.xml
{
    public class XmlEndpoint
    {
        public static readonly XmlEndpoint PAYMENTS = new XmlEndpoint(
            "paymentService",
            "-//Worldpay//DTD Worldpay PaymentService v1//EN",
            "http://dtd.worldpay.com/paymentService_v1.dtd",
            "1.4",
            "https://secure-test.worldpay.com/jsp/merchant/xml/paymentService.jsp",
            "https://secure.worldpay.com/jsp/merchant/xml/paymentService.jsp"
        );

        public static readonly XmlEndpoint BATCH = new XmlEndpoint(
            "batchService",
            "-//Worldpay//DTD Worldpay batchService v1//EN",
            "http://dtd.worldpay.com/batchService_v1.dtd",
            "1.0",
            "https://secure-test.worldpay.com/jsp/merchant/xml/batch.html",
            "https://secure.worldpay.com/jsp/merchant/xml/batch.html"
        );

        private XmlEndpoint(String rootElement, String docTypePublicId, String docTypeSystemId, String version, String sandboxUrl, String productionUrl)
        {
            this.RootElement = rootElement;
            this.DocTypePublicId = docTypePublicId;
            this.DocTypeSystemId = docTypeSystemId;
            this.Version = version;
            this.SandboxUrl = sandboxUrl;
            this.ProductionUrl = productionUrl;
        }

        public String RootElement { get; private set; }
        public String DocTypePublicId { get; private set; }
        public String DocTypeSystemId { get; private set; }
        public String Version { get; private set; }
        public String SandboxUrl { get; private set; }
        public String ProductionUrl { get; private set; }

        public Uri GetUri(GatewayEnvironment environment)
        {
            Uri uri;

            switch (environment)
            {
                case GatewayEnvironment.PRODUCTION:
                    uri = new Uri(ProductionUrl);
                    break;
                case GatewayEnvironment.SANDBOX:
                    uri = new Uri(SandboxUrl);
                    break;
                default:
                    throw new ArgumentException("Unknown gateway environment: " + environment.ToString());
            }

            return uri;
        }

    }
}
