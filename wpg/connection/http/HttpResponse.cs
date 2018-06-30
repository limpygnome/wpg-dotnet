using System.Collections.Generic;

namespace Worldpay
{
    public class HttpResponse
    {
        public HttpResponse(Dictionary<string, string> headers, string body)
        {
            this.Headers = headers;
            this.Body = body;
        }

        public Dictionary<string, string> Headers { get; private set; }
        public string Body { get; private set; }
        public int Length { get { return Body.Length; } }

    }
}
