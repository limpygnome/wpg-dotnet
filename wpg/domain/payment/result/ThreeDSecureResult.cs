using System;
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

    }
}
