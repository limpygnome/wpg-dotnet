using System.Collections.Generic;

namespace Worldpay
{
    public class ISO8583Result
    {
        public ISO8583Result(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public string Code { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            var result = obj as ISO8583Result;
            return result != null &&
                   Code == result.Code &&
                   Description == result.Description;
        }

        public override int GetHashCode()
        {
            var hashCode = 1291371069;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }

    }
}
