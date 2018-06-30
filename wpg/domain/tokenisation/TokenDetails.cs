using System;
using System.Collections.Generic;

namespace Worldpay
{
    public class TokenDetails
    {
        public TokenDetails(string paymentTokenId, DateTime tokenExpiry, string tokenEvent, string eventReference, string eventReason)
        {
            this.PaymentTokenId = paymentTokenId;
            this.TokenExpiry = tokenExpiry;
            this.TokenEvent = tokenEvent;
            this.EventReference = eventReference;
            this.EventReason = eventReason;
        }

        public string PaymentTokenId { get; set; }
        public DateTime TokenExpiry { get; set; }
        public string TokenEvent { get; set; }
        public string EventReference { get; set; }
        public string EventReason { get; set; }

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
