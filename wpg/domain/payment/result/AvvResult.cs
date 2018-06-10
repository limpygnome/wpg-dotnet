using System;
namespace wpg.domain.payment.result
{
    public class AvvResult
    {
        public AvvResult(String addressResultCode, String postCodeResultCode, String cardHolderNameResultCode, String telephoneResultCode, String emailResultCode)
        {
            this.AddressResultCode = addressResultCode;
            this.PostCodeResultCode = postCodeResultCode;
            this.CardHolderNameResultCode = cardHolderNameResultCode;
            this.TelephoneResultCode = telephoneResultCode;
            this.EmailResultCode = emailResultCode;
        }

        public String AddressResultCode { get; set; }
        public String PostCodeResultCode { get; set; }
        public String CardHolderNameResultCode { get; set; }
        public String TelephoneResultCode { get; set; }
        public String EmailResultCode { get; set; }

    }
}
