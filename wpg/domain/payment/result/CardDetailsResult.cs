using System;
using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            var result = obj as CardDetailsResult;
            return result != null &&
                   MaskedCardNumber == result.MaskedCardNumber &&
                   HashedCardNumber == result.HashedCardNumber &&
                   EqualityComparer<long?>.Default.Equals(ExpiryMonth, result.ExpiryMonth) &&
                   EqualityComparer<long?>.Default.Equals(ExpiryYear, result.ExpiryYear) &&
                   IssuerCountryCode == result.IssuerCountryCode &&
                   IssuerName == result.IssuerName &&
                   CardHolderName == result.CardHolderName &&
                   EqualityComparer<CardType?>.Default.Equals(Type, result.Type);
        }

        public override int GetHashCode()
        {
            var hashCode = 914820374;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MaskedCardNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HashedCardNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<long?>.Default.GetHashCode(ExpiryMonth);
            hashCode = hashCode * -1521134295 + EqualityComparer<long?>.Default.GetHashCode(ExpiryYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IssuerCountryCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IssuerName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CardHolderName);
            hashCode = hashCode * -1521134295 + EqualityComparer<CardType?>.Default.GetHashCode(Type);
            return hashCode;
        }

    }
}
