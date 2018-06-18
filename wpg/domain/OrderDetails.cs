using System;
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

    }
}
