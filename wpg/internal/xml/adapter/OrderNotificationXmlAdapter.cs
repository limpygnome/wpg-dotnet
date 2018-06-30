using System.Collections.Generic;
using Worldpay.@internal.xml.serializer;
using Worldpay.@internal.xml.serializer.payment;

namespace Worldpay.@internal.xml.adapter
{
    internal class OrderNotificationXmlAdapter
    {

        public static OrderNotification orderNotification(XmlBuilder builder)
        {
            if (builder.hasE("notify") && builder.hasE("orderStatusEvent"))
            {
                string orderCode = builder.a("orderCode");

                // read payments
                List<XmlBuilder> children = builder.childTags("payment");
                List<Payment> payments = new List<Payment>(children.Count);
                foreach (XmlBuilder childBuilder in children)
                {
                    Payment payment = PaymentSerializer.read(childBuilder);
                    payments.Add(payment);
                }

                // read journal
                Journal journal = null;
                if (builder.hasE("journal"))
                {
                    journal = JournalSerializer.read(builder);
                }

                // give back result
                OrderNotification notification = new OrderNotification(orderCode, payments, journal);
                return notification;
            }
            else
            {
                throw new WpgMalformedException("Not recognized as order status event");
            }
        }

    }
}
