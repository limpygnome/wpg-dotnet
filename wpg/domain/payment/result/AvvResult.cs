using System.Collections.Generic;

namespace Worldpay
{
    public class AvvResult
    {
        public AvvResult(string addressResultCode, string postCodeResultCode, string cardHolderNameResultCode, string telephoneResultCode, string emailResultCode)
        {
            this.AddressResultCode = addressResultCode;
            this.PostCodeResultCode = postCodeResultCode;
            this.CardHolderNameResultCode = cardHolderNameResultCode;
            this.TelephoneResultCode = telephoneResultCode;
            this.EmailResultCode = emailResultCode;
        }

        public string AddressResultCode { get; set; }
        public string PostCodeResultCode { get; set; }
        public string CardHolderNameResultCode { get; set; }
        public string TelephoneResultCode { get; set; }
        public string EmailResultCode { get; set; }

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
