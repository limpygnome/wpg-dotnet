using System.Collections.Generic;

namespace Worldpay
{
    public class ThreeDsDetails
    {
        public ThreeDsDetails(SessionContext sessionContext, string issuerURL, string paRequest)
        {
            this.SessionContext = sessionContext;
            this.IssuerURL = issuerURL;
            this.PaRequest = paRequest;
        }

        public SessionContext SessionContext { get; private set; }
        public string IssuerURL { get; private set; }
        public string PaRequest { get; private set; }

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
