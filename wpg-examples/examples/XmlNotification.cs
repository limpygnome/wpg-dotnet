using System;
using Worldpay;

namespace wpgexamples
{
    public class XmlNotification
    {
        public void Run(string xml)
        {
            OrderNotification notification = new XmlNotificationBuilder().Read(xml);
            foreach (Payment payment in notification.Payments)
            {
                Console.WriteLine(payment.LastEvent);
            }
        }
    }
}
