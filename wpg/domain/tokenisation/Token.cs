using System;
using System.Collections.Generic;

namespace Worldpay
{
    public class Token
    {
        public Token(TokenDetails details, TokenInstrument instrument, string shopperId)
        {
            this.PaymentTokenId = (details != null ? details.PaymentTokenId : null);
            this.Scope = (String.IsNullOrWhiteSpace(shopperId) ? TokenScope.MERCHANT : TokenScope.SHOPPER);
            this.Details = details;
            this.Instrument = instrument;
            this.ShopperId = shopperId;
        }

        public string PaymentTokenId { get; set; }
        public TokenScope Scope { get; set; }
        public TokenDetails Details { get; set; }
        public TokenInstrument Instrument { get; set; }
        public string ShopperId { get; set; }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   PaymentTokenId == token.PaymentTokenId &&
                   Scope == token.Scope &&
                   EqualityComparer<TokenDetails>.Default.Equals(Details, token.Details) &&
                   EqualityComparer<TokenInstrument>.Default.Equals(Instrument, token.Instrument) &&
                   ShopperId == token.ShopperId;
        }

        public override int GetHashCode()
        {
            var hashCode = 723027992;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PaymentTokenId);
            hashCode = hashCode * -1521134295 + Scope.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TokenDetails>.Default.GetHashCode(Details);
            hashCode = hashCode * -1521134295 + EqualityComparer<TokenInstrument>.Default.GetHashCode(Instrument);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShopperId);
            return hashCode;
        }

    }
}
