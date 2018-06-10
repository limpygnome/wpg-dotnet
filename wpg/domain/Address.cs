using System;
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

    }
}
