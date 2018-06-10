using System;
using wpg.connection.http;
namespace wpg.@internal.xml
{
    public class XmlResponse
    {
        public XmlResponse()
        {
        }

        public HttpResponse Response { get; private set; }
        public XmlBuilder Builder { get; private set; }

    }
}
