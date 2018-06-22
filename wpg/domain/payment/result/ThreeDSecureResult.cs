using System;
using System.Collections.Generic;

namespace wpg.domain.payment.result
{
    public class ThreeDSecureResult
    {
        public ThreeDSecureResult(String description, String eci, String cavv)
        {
            this.Description = description;
            this.ECI = eci;
            this.CAVV = cavv;
        }

        public String Description { get; set; }
        public String ECI { get; set; }
        public String CAVV { get; set; }

        public override bool Equals(object obj)
        {
            var result = obj as ThreeDSecureResult;
            return result != null &&
                   Description == result.Description &&
                   ECI == result.ECI &&
                   CAVV == result.CAVV;
        }

        public override int GetHashCode()
        {
            var hashCode = 610603306;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ECI);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CAVV);
            return hashCode;
        }

    }
}
