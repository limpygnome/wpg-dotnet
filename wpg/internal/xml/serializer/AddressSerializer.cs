using System;
using wpg.domain;
namespace wpg.@internal.xml.serializer
{
    public class AddressSerializer
    {

        public static void decorateOrder(XmlBuildParams buildParams, Address billingAddress, Address shippingAddress)
        {
            XmlBuilder builder = buildParams.Builder;

            if (shippingAddress != null)
            {
                decorateAddress("shippingAddress", builder, shippingAddress);
            }
            if (billingAddress != null)
            {
                decorateAddress("billingAddress", builder, billingAddress);
            }
        }

        public static void decorateCurrentElement(XmlBuildParams buildParams, Address address)
        {
            if (address != null)
            {
                XmlBuilder builder = buildParams.Builder;
                decorateAddress(null, builder, address);
            }
        }

        private static void decorateAddress(String elementName, XmlBuilder builder, Address address)
        {
            if (elementName != null)
            {
                builder.e(elementName);
            }

            builder.e("address");

            // TODO need to determine what is actually mandatory
            if (address.Address1 == null)
            {
                throw new ArgumentException("Address 1 is mandatory for " + elementName);
            }
            else if (address.PostalCode == null)
            {
                throw new ArgumentException("Postal code is mandatory for " + elementName);
            }
            else if (address.CountryCode == null)
            {
                throw new ArgumentException("Country code is mandatory for " + elementName);
            }

            if (address.FirstName != null)
            {
                builder.e("firstName").cdata(address.FirstName).up();
            }
            if (address.LastName != null)
            {
                builder.e("lastName").cdata(address.LastName).up();
            }
            builder.e("address1").cdata(address.Address1).up();
            if (address.Address2 != null)
            {
                builder.e("address2").cdata(address.Address2).up();
            }
            if (address.Address3 != null)
            {
                builder.e("address3").cdata(address.Address3).up();
            }
            builder.e("postalCode").cdata(address.PostalCode).up();
            if (address.City != null)
            {
                builder.e("city").cdata(address.City).up();
            }
            if (address.State != null)
            {
                builder.e("state").cdata(address.State).up();
            }
            builder.e("countryCode").cdata(address.CountryCode).up();
            if (address.TelephoneNumber != null)
            {
                builder.e("telephoneNumber").cdata(address.TelephoneNumber).up();
            }

            builder.up()
                    .up();
        }

        public static Address read(XmlBuilder builder)
        {
            String firstName = builder.getCdata("firstName");
            String lastName = builder.getCdata("lastName");
            String address1 = builder.getCdata("address1");
            String address2 = builder.getCdata("address2");
            String address3 = builder.getCdata("address3");
            String postcode = builder.getCdata("postalCode");
            String city = builder.getCdata("city");
            String state = builder.getCdata("state");
            String countryCode = builder.getCdata("countryCode");
            String telephoneNumber = builder.getCdata("telephoneNumber");

            Address address = new Address(firstName, lastName, address1, address2, address3, postcode, city, state, countryCode, telephoneNumber);
            return address;
        }

    }
}
