using System;
namespace wpg.domain.payment.result
{
    public class ISO8583Result
    {
        public ISO8583Result(String code, String description)
        {
            this.Code = code;
            this.Description = description;
        }

        public String Code { get; set; }
        public String Description { get; set; }
    }
}
