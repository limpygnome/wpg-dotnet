using System.Collections.Generic;
using wpg.domain.payment;
using wpg.domain.journal;

namespace wpg.domain.notification
{
    public class OrderNotification
    {
        public OrderNotification(string orderCode, List<Payment> payments, Journal journal)
        {
            this.OrderCode = orderCode;
            this.Payments = payments;
            this.Journal = journal;
        }

        public string OrderCode { get; private set; }
        public List<Payment> Payments { get; private set; }
        public Journal Journal { get; private set; }

        public override bool Equals(object obj)
        {
            var notification = obj as OrderNotification;
            return notification != null &&
                   OrderCode == notification.OrderCode &&
                   EqualityComparer<List<Payment>>.Default.Equals(Payments, notification.Payments) &&
                   EqualityComparer<Journal>.Default.Equals(Journal, notification.Journal);
        }

        public override int GetHashCode()
        {
            var hashCode = -492485596;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Payment>>.Default.GetHashCode(Payments);
            hashCode = hashCode * -1521134295 + EqualityComparer<Journal>.Default.GetHashCode(Journal);
            return hashCode;
        }

    }
}
