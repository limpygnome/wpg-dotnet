using System;
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

    }
}
