using System;
namespace wpg.domain.payment.result
{
    public class PayoutAuthorisationResult
    {
        public PayoutAuthorisationResult(String authorisationId)
        {
            this.AuthorisationId = authorisationId;
        }

        public String AuthorisationId { get; private set; }

    }
}
