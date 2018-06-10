using System;
namespace wpg.domain.payment
{
    public class Amount
    {
        public Amount(String currency, long exponent, long value) : this(currency, exponent, value, null) { }

        public Amount(String currency, long exponent, long value, DebitCreditIndicator? debitCreditIndicator)
        {
            this.Currency = currency;
            this.Exponent = exponent;
            this.Value = value;
            this.DebitCreditIndicator = debitCreditIndicator;
        }

        public String Currency { get; set; }
        public long Exponent { get; set; }
        public long Value { get; set; }
        public DebitCreditIndicator? DebitCreditIndicator { get; private set; }

    }
}
