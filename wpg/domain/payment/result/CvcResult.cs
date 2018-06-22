using System;
using System.Collections.Generic;

namespace wpg.domain.payment.result
{
    public class CvcResult
    {
        public CvcResult(String description)
        {
            this.Description = description;
        }

        public String Description { get; set; }

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
