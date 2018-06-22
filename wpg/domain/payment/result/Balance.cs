using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            var balance = obj as Balance;
            return balance != null &&
                   AccountType == balance.AccountType &&
                   EqualityComparer<Amount>.Default.Equals(Amount, balance.Amount);
        }

        public override int GetHashCode()
        {
            var hashCode = -29554179;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AccountType);
            hashCode = hashCode * -1521134295 + EqualityComparer<Amount>.Default.GetHashCode(Amount);
            return hashCode;
        }

    }
}
