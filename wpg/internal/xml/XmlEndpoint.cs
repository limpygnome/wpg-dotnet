using System;

namespace Worldpay.@internal.xml
{
    internal class XmlEndpoint
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

        private XmlEndpoint(string rootElement, string docTypePublicId, string docTypeSystemId, string version, string sandboxUrl, string productionUrl)
        {
            this.RootElement = rootElement;
            this.DocTypePublicId = docTypePublicId;
            this.DocTypeSystemId = docTypeSystemId;
            this.Version = version;
            this.SandboxUrl = sandboxUrl;
            this.ProductionUrl = productionUrl;
        }

        public string RootElement { get; private set; }
        public string DocTypePublicId { get; private set; }
        public string DocTypeSystemId { get; private set; }
        public string Version { get; private set; }
        public string SandboxUrl { get; private set; }
        public string ProductionUrl { get; private set; }

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
