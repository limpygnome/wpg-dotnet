using System;
namespace wpg.domain
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

    }
}
