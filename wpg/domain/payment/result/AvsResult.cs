using System;
using System.Collections.Generic;

namespace wpg.domain.payment.result
{
    public class AvsResult
    {
        public AvsResult(String avsResultCode)
        {
            this.AvsResultCode = avsResultCode;
        }

        public String AvsResultCode { get; set; }

        public override bool Equals(object obj)
        {
            var result = obj as AvsResult;
            return result != null &&
                   AvsResultCode == result.AvsResultCode;
        }

        public override int GetHashCode()
        {
            return -1184249157 + EqualityComparer<string>.Default.GetHashCode(AvsResultCode);
        }

    }
}
