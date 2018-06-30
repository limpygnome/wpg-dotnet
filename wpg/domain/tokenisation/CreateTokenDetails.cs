using System;
using System.Collections.Generic;

namespace Worldpay
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
        public string EventReference { get; set; }
        public string Reason { get; set; }
        public int? ShortLifeMins { get; set; }
        public DateTime? Expiry { get; set; }

        public override bool Equals(object obj)
        {
            var details = obj as CreateTokenDetails;
            return details != null &&
                   Scope == details.Scope &&
                   EventReference == details.EventReference &&
                   Reason == details.Reason &&
                   EqualityComparer<int?>.Default.Equals(ShortLifeMins, details.ShortLifeMins) &&
                   EqualityComparer<DateTime?>.Default.Equals(Expiry, details.Expiry);
        }

        public override int GetHashCode()
        {
            var hashCode = 1798268540;
            hashCode = hashCode * -1521134295 + Scope.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EventReference);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Reason);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(ShortLifeMins);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(Expiry);
            return hashCode;
        }

    }
}
