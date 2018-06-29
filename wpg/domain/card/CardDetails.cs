using System;
using System.Collections.Generic;

namespace wpg.domain.card
{
    public class CardDetails
    {
        public CardDetails(String cardNumber, long? expiryMonth, long? expiryYear, String cardHolderName)
        {
            this.CardNumber = cardNumber;
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
            this.CardHolderName = cardHolderName;
        }

        public CardDetails(String cardNumber, long? expiryMonth, long? expiryYear, String cardHolderName, String cvc)
        {
            this.CardNumber = cardNumber;
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
            this.CardHolderName = cardHolderName;
            this.CVC = cvc;
        }

        public CardDetails(String cardNumber, long? expiryMonth, long? expiryYear, String cardHolderName, String cvc, Address cardHolderAddress)
        {
            this.CardNumber = cardNumber;
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
            this.CardHolderName = cardHolderName;
            this.CVC = cvc;
            this.CardHolderAddress = cardHolderAddress;
        }

        public CardDetails(String cardNumber, long? expiryMonth, long? expiryYear, String cardHolderName, String cvc, Address cardHolderAddress, String encryptedCardNumber)
        {
            this.CardNumber = cardNumber;
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
            this.CardHolderName = cardHolderName;
            this.CVC = cvc;
            this.CardHolderAddress = cardHolderAddress;
            this.EncryptedCardNumber = encryptedCardNumber;
        }

        public String CardNumber { get; set; }
        public long? ExpiryMonth { get; set; }
        public long? ExpiryYear { get; set; }
        public String CardHolderName { get; set; }
        public String CVC { get; set; }
        public Address CardHolderAddress { get; set; }
        public String EncryptedCardNumber { get; private set; }

        public bool isAnythingDefined()
        {
            return !String.IsNullOrWhiteSpace(CardNumber)
                          || ExpiryMonth != null
                          || ExpiryYear != null
                          || !String.IsNullOrWhiteSpace(CardHolderName)
                          || !String.IsNullOrWhiteSpace(CVC)
                          || CardHolderAddress != null;
        }

        public override bool Equals(object obj)
        {
            var details = obj as CardDetails;
            return details != null &&
                   CardNumber == details.CardNumber &&
                   EqualityComparer<long?>.Default.Equals(ExpiryMonth, details.ExpiryMonth) &&
                   EqualityComparer<long?>.Default.Equals(ExpiryYear, details.ExpiryYear) &&
                   CardHolderName == details.CardHolderName &&
                   CVC == details.CVC &&
                   EqualityComparer<Address>.Default.Equals(CardHolderAddress, details.CardHolderAddress) &&
                   EncryptedCardNumber == details.EncryptedCardNumber;
        }

        public override int GetHashCode()
        {
            var hashCode = 766638519;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CardNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<long?>.Default.GetHashCode(ExpiryMonth);
            hashCode = hashCode * -1521134295 + EqualityComparer<long?>.Default.GetHashCode(ExpiryYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CardHolderName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CVC);
            hashCode = hashCode * -1521134295 + EqualityComparer<Address>.Default.GetHashCode(CardHolderAddress);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EncryptedCardNumber);
            return hashCode;
        }

    }
}
