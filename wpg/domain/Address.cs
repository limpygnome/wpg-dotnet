using System;
using System.Collections.Generic;

namespace wpg.domain
{
    public class Address
    {
        public Address(String address1, String city, String postalCode, String countryCode)
        {
            this.Address1 = address1;
            this.City = city;
            this.PostalCode = postalCode;
            this.CountryCode = countryCode;
        }

        public Address(String firstName, String lastName, String address1, String address2, String address3, String postalCode, String city, String state, String countryCode, String telephoneNumber)
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

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String Address3 { get; set; }
        public String PostalCode { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String CountryCode { get; set; }
        public String TelephoneNumber { get; set; }

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
