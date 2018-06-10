using System;
namespace wpg.domain.payment.result
{
    public class Balance
    {
        public Balance(String accountType, Amount amount)
        {
            this.AccountType = accountType;
            this.Amount = amount;
        }

        public String AccountType { get; private set; }
        public Amount Amount { get; private set; }
    }
}
