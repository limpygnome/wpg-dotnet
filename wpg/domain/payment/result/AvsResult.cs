using System;
namespace wpg.domain.payment.result
{
    public class AvsResult
    {
        public AvsResult(String avsResultCode)
        {
            this.AvsResultCode = avsResultCode;
        }

        public String AvsResultCode { get; set; }

    }
}
