using System.Collections.Generic;
using wpg.domain.payment;
using wpg.domain.journal;
using wpg.domain.notification;
using wpg.exception;
using wpg.@internal.xml.serializer;
using wpg.@internal.xml.serializer.payment;

namespace wpg.@internal.xml.adapter
{
    public class OrderNotificationXmlAdapter
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
