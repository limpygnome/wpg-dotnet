using System;
using Worldpay.@internal.validation;
using Worldpay.@internal.xml;
using Worldpay.@internal.xml.adapter;
using Worldpay.@internal.xml.serializer;
using Worldpay.@internal.xml.serializer.payment.tokenisation;

namespace Worldpay
{
    public class PayPalPaymentRequest : XmlRequest<RedirectUrl>
    {

        public PayPalPaymentRequest() { }

        public PayPalPaymentRequest(OrderDetails orderDetails, Shopper shopper, string successURL, string failureURL, string cancelURL)
        {
            this.OrderDetails = orderDetails;
            this.Shopper = shopper;
            this.SuccessURL = successURL;
            this.FailureURL = failureURL;
            this.CancelURL = cancelURL;
        }

        public PayPalPaymentRequest(OrderDetails orderDetails, Shopper shopper, string resultURL)
        {
            this.OrderDetails = orderDetails;
            this.Shopper = shopper;
            this.SetResultURLs(resultURL);
        }

        // Mandatory
        public OrderDetails OrderDetails { get; set; }
        public Shopper Shopper { get; set; }
        public string SuccessURL { get; set; }
        public string FailureURL { get; set; }
        public string CancelURL { get; set; }

        // Optional
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public string LanguageCode { get; set; }
        public CreateTokenDetails CreateTokenDetails { get; set; }

        public PayPalPaymentRequest SetResultURLs(string resultUrl)
        {
            this.SuccessURL = resultUrl;
            this.FailureURL = resultUrl;
            this.CancelURL = resultUrl;
            return this;
        }

        public PayPalPaymentRequest SetLanguage(PayPalLanguage language)
        {
            switch (language)
            {
                case PayPalLanguage.ENGLISH:
                    LanguageCode = "gb";
                    break;
                case PayPalLanguage.GERMAN:
                    LanguageCode = "de";
                    break;
                case PayPalLanguage.FRENCH:
                    LanguageCode = "fr";
                    break;
                case PayPalLanguage.SPANISH:
                    LanguageCode = "es";
                    break;
                case PayPalLanguage.POLISH:
                    LanguageCode = "pl";
                    break;
                case PayPalLanguage.CHINESE:
                    LanguageCode = "zh";
                    break;
                default:
                    throw new ArgumentException("Language '" + language.ToString() + "' not supported");
            }
            return this;
        }

        internal override void Validate(XmlBuildParams buildParams)
        {
            Assert.notNull(OrderDetails, "Order details are mandatory");
            Assert.notNull(Shopper, "Shopper details are mandatory");
            Assert.notNull(Shopper.Email, "Shopper's e-mail is mandatory");
            Assert.notNull(SuccessURL, "Success URL is mandatory");
            Assert.notNull(SuccessURL, "Failure URL is mandatory");
            Assert.notNull(SuccessURL, "Cancel URL is mandatory");

            if (this.CreateTokenDetails != null)
            {
                Assert.notEmpty(Shopper.ShopperId, "Shopper ID is required for tokenised payments");
            }
        }

        internal override void Build(XmlBuildParams buildParams)
        {
            OrderDetailsSerializer.decorateAndStartOrder(buildParams, OrderDetails);

            XmlBuilder builder = buildParams.Builder;

            // PayPal information
            // --- Language
            if (!String.IsNullOrWhiteSpace(LanguageCode))
            {
                builder.a("shopperLanguageCode", LanguageCode);
            }

            // -- PayPal details
            builder.e("paymentDetails").e("PAYPAL-EXPRESS");

            if (CreateTokenDetails != null)
            {
                builder.a("firstInBillingRun", "true");
            }

            builder.e("successURL").cdata(SuccessURL).up();
            builder.e("failureURL").cdata(FailureURL).up();
            builder.e("cancelURL").cdata(CancelURL).up();

            builder.up().up();

            // Continue generic information
            ShopperSerializer.decorateOrder(buildParams, Shopper);
            AddressSerializer.decorateOrder(buildParams, BillingAddress, ShippingAddress);
            CreateTokenDetailsSerializer.decorateOrder(buildParams, CreateTokenDetails);

            OrderDetailsSerializer.decorateFinishOrder(buildParams);
        }

        internal override RedirectUrl Adapt(XmlResponse response)
        {
            RedirectUrlXmlAdapter adapter = new RedirectUrlXmlAdapter();
            RedirectUrl redirectUrl = adapter.read(response);
            return redirectUrl;
        }

    }
}
