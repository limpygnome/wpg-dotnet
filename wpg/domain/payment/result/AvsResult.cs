using System.Collections.Generic;

namespace Worldpay
{
    public class AvsResult
    {
        public AvsResult(string avsResultCode)
        {
            this.AvsResultCode = avsResultCode;
        }

        public string AvsResultCode { get; set; }

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
