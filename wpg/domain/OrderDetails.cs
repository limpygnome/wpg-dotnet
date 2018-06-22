using System;
using System.Collections.Generic;
using wpg.builder;
using wpg.domain.payment;
namespace wpg.domain
{
    public class OrderDetails
    {
        private const int ID_MAX_LEN = 32;

        public OrderDetails(string description, Amount amount) : this(null, description, amount) { }

        public OrderDetails(string orderCode, string description, Amount amount)
        {
            this.OrderCode = orderCode;
            this.Description = description;
            this.Amount = amount;

            if (orderCode == null)
            {
                this.OrderCode = RandomIdentifier.generate(ID_MAX_LEN);
            }
        }

        public string OrderCode { get; set; }
        public string Description { get; set; }
        public Amount Amount { get; set; }

        public override bool Equals(object obj)
        {
            var details = obj as OrderDetails;
            return details != null &&
                   OrderCode == details.OrderCode &&
                   Description == details.Description &&
                   EqualityComparer<Amount>.Default.Equals(Amount, details.Amount);
        }

        public override int GetHashCode()
        {
            var hashCode = 1562747568;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<Amount>.Default.GetHashCode(Amount);
            return hashCode;
        }

    }
}
