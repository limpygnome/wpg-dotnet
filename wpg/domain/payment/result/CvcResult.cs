using System;
namespace wpg.domain.payment.result
{
    public class CvcResult
    {
        public CvcResult(String description)
        {
            this.Description = description;
        }

        public String Description { get; set; }
    }
}
