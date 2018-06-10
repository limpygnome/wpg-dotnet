using System;
using System.Collections.Generic;

namespace wpg.connection.http
{
    public class HttpResponse
    {
        public HttpResponse(Dictionary<String, String> headers, String body)
        {
            this.Headers = headers;
            this.Body = body;
        }

        public Dictionary<String, String> Headers { get; private set; }
        public string Body { get; private set; }
        public int Length { get { return Body.Length; } }

    }
}
