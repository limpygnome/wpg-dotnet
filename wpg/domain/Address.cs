using System.Collections.Generic;

namespace Worldpay
{
    public class Address
    {
        public Address(string address1, string city, string postalCode, string countryCode)
        {
            this.Address1 = address1;
            this.City = city;
            this.PostalCode = postalCode;
            this.CountryCode = countryCode;
        }

        public Address(string firstName, string lastName, string address1, string address2, string address3, string postalCode, string city, string state, string countryCode, string telephoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address1 = address1;
            this.Address2 = address2;
            this.Address3 = address3;
            this.PostalCode = postalCode;
            this.City = city;
            this.State = state;
            this.CountryCode = countryCode;
            this.TelephoneNumber = telephoneNumber;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string TelephoneNumber { get; set; }

        public override bool Equals(object obj)
        {
            var address = obj as Address;
            return address != null &&
                   FirstName == address.FirstName &&
                   LastName == address.LastName &&
                   Address1 == address.Address1 &&
                   Address2 == address.Address2 &&
                   Address3 == address.Address3 &&
                   PostalCode == address.PostalCode &&
                   City == address.City &&
                   State == address.State &&
                   CountryCode == address.CountryCode &&
                   TelephoneNumber == address.TelephoneNumber;
        }

        public override int GetHashCode()
        {
            var hashCode = -772988318;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address1);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address2);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address3);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PostalCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CountryCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TelephoneNumber);
            return hashCode;
        }

    }
}
