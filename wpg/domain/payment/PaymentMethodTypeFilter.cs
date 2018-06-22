using System;
using System.Collections.Generic;

namespace wpg.domain.payment
{
    public class PaymentMethodTypeFilter
    {
        public PaymentMethodTypeFilter()
        {
            Included = new LinkedList<PaymentMethodType>();
            Excluded = new LinkedList<PaymentMethodType>();
        }

        public LinkedList<PaymentMethodType> Included { get; set; }
        public LinkedList<PaymentMethodType> Excluded { get; set; }

        public override bool Equals(object obj)
        {
            var filter = obj as PaymentMethodTypeFilter;
            return filter != null &&
                   EqualityComparer<LinkedList<PaymentMethodType>>.Default.Equals(Included, filter.Included) &&
                   EqualityComparer<LinkedList<PaymentMethodType>>.Default.Equals(Excluded, filter.Excluded);
        }

        public override int GetHashCode()
        {
            var hashCode = -375991912;
            hashCode = hashCode * -1521134295 + EqualityComparer<LinkedList<PaymentMethodType>>.Default.GetHashCode(Included);
            hashCode = hashCode * -1521134295 + EqualityComparer<LinkedList<PaymentMethodType>>.Default.GetHashCode(Excluded);
            return hashCode;
        }

    }
}
