using System;
using wpg.domain.card;

namespace wpg.domain.payment.result
{
    public class CardDetailsResult
    {
        public CardDetailsResult(string maskedCardNumber, string hashedCardNumber, long? expiryMonth, long? expiryYear, string issuerCountryCode, string issuerName, string cardHolderName, CardType? type)
        {
            this.MaskedCardNumber = maskedCardNumber;
            this.HashedCardNumber = hashedCardNumber;
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
            this.IssuerCountryCode = issuerCountryCode;
            this.IssuerName = issuerName;
            this.CardHolderName = cardHolderName;
            this.Type = type;
        }

        public String MaskedCardNumber { get; private set; }
        public String HashedCardNumber { get; private set; }
        public long? ExpiryMonth { get; private set; }
        public long? ExpiryYear { get; private set; }
        public string IssuerCountryCode { get; private set; }
        public string IssuerName { get; private set; }
        public string CardHolderName { get; private set; }
        public CardType? Type { get; private set; }

    }
}
