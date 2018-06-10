using System;
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

    }
}
