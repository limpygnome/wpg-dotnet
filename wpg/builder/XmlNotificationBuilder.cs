using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;

namespace Worldpay
{
    public class XmlNotificationBuilder
    {
        
        public OrderNotification Read(string xml)
        {
            try
            {
                XmlBuilder builder = XmlBuilder.parse(XmlEndpoint.PAYMENTS, xml);
                OrderNotification notification = OrderNotificationXmlAdapter.orderNotification(builder);
                return notification;
            }
            catch (WpgRequestException e)
            {
                throw new WpgMalformedException("Malformed order notification XML", xml, e);
            }
        }

    }
}
