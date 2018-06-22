using wpg.domain.notification;
using wpg.exception;
using wpg.@internal.xml;
using wpg.@internal.xml.adapter;

namespace wpg.builder
{
    public class XmlNotificationBuilder
    {
        
        public OrderNotification read(string xml)
        {
            try
            {
                XmlBuilder builder = XmlBuilder.parse(XmlEndpoint.PAYMENTS, xml);
                OrderNotification notification = OrderNotificationXmlAdapter.orderNotification(builder);
                return notification;
            }
            catch (WpgRequestException e)
            {
                throw new WpgMalformedException("Malformed order notification XML", xml);
            }
        }

    }
}
