using System;
using wpg.connection.http;
using wpg.domain.redirect;
using wpg.exception;

namespace wpg.@internal.xml.adapter
{
    public class RedirectUrlXmlAdapter
    {

        public RedirectUrl read(XmlResponse response)
        {
            HttpResponse httpResponse = response.Response;
            String xml = httpResponse.Body;
            XmlBuilder builder = XmlBuilder.parse(XmlEndpoint.PAYMENTS, xml);

            if (builder.hasE("reply") && builder.hasE("orderStatus") && builder.hasE("reference"))
            {
                String url = builder.cdata();
                RedirectUrl redirectUrl = new RedirectUrl(url);
                return redirectUrl;
            }
            else
            {
                throw new WpgMalformedException(response.Response);
            }
        }

    }
}
