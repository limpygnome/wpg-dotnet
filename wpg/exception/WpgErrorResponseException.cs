namespace Worldpay
{
    public class WpgErrorResponseException : WpgException
    {
        public WpgErrorResponseException(long gatewayErrorCode, string gatewayErrorMessage, HttpResponse response)
            : base("Gateway error - code: " + gatewayErrorCode + ", message: " + gatewayErrorMessage)
        {
            this.GatewayErrorCode = gatewayErrorCode;
            this.GatewayErrorMessage = gatewayErrorMessage;
            this.Response = response;
        }

        public long GatewayErrorCode { get; private set; }
        public string GatewayErrorMessage { get; private set; }
        public HttpResponse Response { get; private set; }

    }
}
