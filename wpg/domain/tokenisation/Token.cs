using System;
namespace wpg.domain.tokenisation
{
    public class Token
    {
        public Token(TokenDetails details, TokenInstrument instrument, String shopperId)
        {
            this.PaymentTokenId = (details != null ? details.PaymentTokenId : null);
            this.Scope = (String.IsNullOrWhiteSpace(shopperId) ? TokenScope.MERCHANT : TokenScope.SHOPPER);
            this.Details = details;
            this.Instrument = instrument;
            this.ShopperId = shopperId;
        }

        public String PaymentTokenId { get; set; }
        public TokenScope Scope { get; set; }
        public TokenDetails Details { get; set; }
        public TokenInstrument Instrument { get; set; }
        public String ShopperId { get; set; }

    }
}
