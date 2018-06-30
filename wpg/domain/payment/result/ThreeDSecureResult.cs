using System.Collections.Generic;

namespace Worldpay
{
    public class ThreeDSecureResult
    {
        public ThreeDSecureResult(string description, string eci, string cavv)
        {
            this.Description = description;
            this.ECI = eci;
            this.CAVV = cavv;
        }

        public string Description { get; set; }
        public string ECI { get; set; }
        public string CAVV { get; set; }

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
