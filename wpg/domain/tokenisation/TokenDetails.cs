using System;
using System.Collections.Generic;

namespace wpg.domain.tokenisation
{
    public class TokenDetails
    {
        public TokenDetails(String paymentTokenId, DateTime tokenExpiry, String tokenEvent, String eventReference, String eventReason)
        {
            this.PaymentTokenId = paymentTokenId;
            this.TokenExpiry = tokenExpiry;
            this.TokenEvent = tokenEvent;
            this.EventReference = eventReference;
            this.EventReason = eventReason;
        }

        public String PaymentTokenId { get; set; }
        public DateTime TokenExpiry { get; set; }
        public String TokenEvent { get; set; }
        public String EventReference { get; set; }
        public String EventReason { get; set; }

        public override bool Equals(object obj)
        {
            var details = obj as TokenDetails;
            return details != null &&
                   PaymentTokenId == details.PaymentTokenId &&
                   TokenExpiry == details.TokenExpiry &&
                   TokenEvent == details.TokenEvent &&
                   EventReference == details.EventReference &&
                   EventReason == details.EventReason;
        }

        public override int GetHashCode()
        {
            var hashCode = 1656467417;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PaymentTokenId);
            hashCode = hashCode * -1521134295 + TokenExpiry.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TokenEvent);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EventReference);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EventReason);
            return hashCode;
        }

    }
}
