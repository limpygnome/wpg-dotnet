using System;
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
    }
}
