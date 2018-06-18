using System;
using wpg.domain.tokenisation;
namespace wpg.domain.tokenisation
{
    public class CreateTokenDetails
    {
        public CreateTokenDetails()
        {
        }

        public CreateTokenDetails(string eventReference, string reason) : this(null, eventReference, reason, null, null) { }

        public CreateTokenDetails(TokenScope? scope, string eventReference, string reason) : this(scope, eventReference, reason, null, null) { }

        public CreateTokenDetails(TokenScope? scope, string eventReference, string reason, DateTime? expiry) : this(scope, eventReference, reason, null, expiry) { }

        public CreateTokenDetails(TokenScope? scope, string eventReference, string reason, int? shortLifeMins) : this(scope, eventReference, reason, shortLifeMins, null) { }

        private CreateTokenDetails(TokenScope? scope, string eventReference, string reason, int? shortLifeMins, DateTime? expiry)
        {
            this.Scope = scope ?? TokenScope.SHOPPER;
            this.EventReference = eventReference;
            this.Reason = reason;
            this.ShortLifeMins = shortLifeMins;
            this.Expiry = expiry;
        }

        public TokenScope Scope { get; set; }
        public String EventReference { get; set; }
        public String Reason { get; set; }
        public int? ShortLifeMins { get; set; }
        public DateTime? Expiry { get; set; }

    }
}
