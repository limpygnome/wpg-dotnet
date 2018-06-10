using System;
namespace wpg.domain.payment.threeds
{
    public class ThreeDsDetails
    {
        public ThreeDsDetails(String issuerURL, String paRequest)
        {
            this.IssuerURL = issuerURL;
            this.PaRequest = paRequest;
        }

        public String IssuerURL { get; private set; }
        public String PaRequest { get; private set; }
    }
}
