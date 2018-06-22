using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            var result = obj as AvvResult;
            return result != null &&
                   AddressResultCode == result.AddressResultCode &&
                   PostCodeResultCode == result.PostCodeResultCode &&
                   CardHolderNameResultCode == result.CardHolderNameResultCode &&
                   TelephoneResultCode == result.TelephoneResultCode &&
                   EmailResultCode == result.EmailResultCode;
        }

        public override int GetHashCode()
        {
            var hashCode = 135254621;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AddressResultCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PostCodeResultCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CardHolderNameResultCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TelephoneResultCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EmailResultCode);
            return hashCode;
        }

    }
}
