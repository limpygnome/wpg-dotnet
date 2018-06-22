using System;
using System.Collections.Generic;
using wpg.domain.payment.result;

namespace wpg.domain.tokenisation
{
    public class TokenCardDetails : TokenInstrument
    {
        public TokenCardDetails(String cardBrand, String cardSubBrand, String issuerCountryCode, String obfuscatedCardNumber, CardDetailsResult cardDetailsResult)
        {
            this.CardBrand = cardBrand;
            this.CardSubBrand = cardSubBrand;
            this.IssuerCountryCode = issuerCountryCode;
            this.ObfuscatedCardNumber = obfuscatedCardNumber;
            this.CardDetailsResult = cardDetailsResult;
        }

        public string CardBrand { get; private set; }
        public string CardSubBrand { get; private set; }
        public string IssuerCountryCode { get; private set; }
        public string ObfuscatedCardNumber { get; private set; }
        public CardDetailsResult CardDetailsResult { get; private set; }

        public override bool Equals(object obj)
        {
            var details = obj as TokenCardDetails;
            return details != null &&
                   CardBrand == details.CardBrand &&
                   CardSubBrand == details.CardSubBrand &&
                   IssuerCountryCode == details.IssuerCountryCode &&
                   ObfuscatedCardNumber == details.ObfuscatedCardNumber &&
                   EqualityComparer<CardDetailsResult>.Default.Equals(CardDetailsResult, details.CardDetailsResult);
        }

        public override int GetHashCode()
        {
            var hashCode = 80415371;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CardBrand);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CardSubBrand);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IssuerCountryCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ObfuscatedCardNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<CardDetailsResult>.Default.GetHashCode(CardDetailsResult);
            return hashCode;
        }

    }
}
