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

    }
}
