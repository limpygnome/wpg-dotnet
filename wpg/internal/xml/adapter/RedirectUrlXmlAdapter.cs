namespace Worldpay.@internal.xml.adapter
{
    internal class RedirectUrlXmlAdapter
    {

        public RedirectUrl read(XmlResponse response)
        {
            HttpResponse httpResponse = response.Response;
            string xml = httpResponse.Body;
            XmlBuilder builder = XmlBuilder.parse(XmlEndpoint.PAYMENTS, xml);

            if (builder.hasE("reply") && builder.hasE("orderStatus") && builder.hasE("reference"))
            {
                string url = builder.cdata();
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
