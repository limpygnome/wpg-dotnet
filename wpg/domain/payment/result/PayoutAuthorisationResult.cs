using System.Collections.Generic;

namespace Worldpay
{
    public class PayoutAuthorisationResult
    {
        public PayoutAuthorisationResult(string authorisationId)
        {
            this.AuthorisationId = authorisationId;
        }

        public string AuthorisationId { get; private set; }

        public override bool Equals(object obj)
        {
            var result = obj as PayoutAuthorisationResult;
            return result != null &&
                   AuthorisationId == result.AuthorisationId;
        }

        public override int GetHashCode()
        {
            return -200721578 + EqualityComparer<string>.Default.GetHashCode(AuthorisationId);
        }

    }
}
