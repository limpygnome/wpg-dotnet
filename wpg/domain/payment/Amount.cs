using System.Collections.Generic;

namespace Worldpay
{
    public class Amount
    {
        public Amount(string currencyCode, long exponent, long value) : this(currencyCode, exponent, value, null) { }

        public Amount(string currencyCode, long exponent, long value, DebitCreditIndicator? debitCreditIndicator)
        {
            this.CurrencyCode = currencyCode;
            this.Exponent = exponent;
            this.Value = value;
            this.DebitCreditIndicator = debitCreditIndicator;
        }

        public string CurrencyCode { get; set; }
        public long Exponent { get; set; }
        public long Value { get; set; }
        public DebitCreditIndicator? DebitCreditIndicator { get; private set; }


        public override bool Equals(object obj)
        {
            var amount = obj as Amount;
            return amount != null &&
                   CurrencyCode == amount.CurrencyCode &&
                   Exponent == amount.Exponent &&
                   Value == amount.Value &&
                   EqualityComparer<DebitCreditIndicator?>.Default.Equals(DebitCreditIndicator, amount.DebitCreditIndicator);
        }

        public override int GetHashCode()
        {
            var hashCode = -819947861;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CurrencyCode);
            hashCode = hashCode * -1521134295 + Exponent.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DebitCreditIndicator?>.Default.GetHashCode(DebitCreditIndicator);
            return hashCode;
        }

    }
}
