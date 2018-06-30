using System.Collections.Generic;

namespace Worldpay
{
    public class CvcResult
    {
        public CvcResult(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            var result = obj as CvcResult;
            return result != null &&
                   Description == result.Description;
        }

        public override int GetHashCode()
        {
            return -1440511887 + EqualityComparer<string>.Default.GetHashCode(Description);
        }

    }
}
