using System;
using System.Collections.Generic;
using wpg.connection;

namespace wpg.domain.payment.threeds
{
    public class ThreeDsDetails
    {
        public ThreeDsDetails(SessionContext sessionContext, String issuerURL, String paRequest)
        {
            this.SessionContext = sessionContext;
            this.IssuerURL = issuerURL;
            this.PaRequest = paRequest;
        }

        public SessionContext SessionContext { get; private set; }
        public String IssuerURL { get; private set; }
        public String PaRequest { get; private set; }

        public override bool Equals(object obj)
        {
            var details = obj as ThreeDsDetails;
            return details != null &&
                   EqualityComparer<SessionContext>.Default.Equals(SessionContext, details.SessionContext) &&
                   IssuerURL == details.IssuerURL &&
                   PaRequest == details.PaRequest;
        }

        public override int GetHashCode()
        {
            var hashCode = 1385606692;
            hashCode = hashCode * -1521134295 + EqualityComparer<SessionContext>.Default.GetHashCode(SessionContext);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IssuerURL);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PaRequest);
            return hashCode;
        }

    }
}
