using System;

namespace Worldpay.@internal.xml.serializer
{
    internal class ShopperSerializer
    {

        public static void decorateOrder(XmlBuildParams buildParams, Shopper shopper)
        {
            if (shopper != null)
            {
                XmlBuilder builder = buildParams.Builder;

                // Get to shopper element
                builder.e("shopper");

                // Append details
                if (shopper.Email != null)
                {
                    builder.e("shopperEmailAddress").cdata(shopper.Email).up();
                }

                if (shopper.ShopperId != null)
                {
                    builder.e("authenticatedShopperID").cdata(shopper.ShopperId).up();
                }

                ShopperBrowser browser = shopper.Browser;
                decorateBrowser(builder, browser);

                // Reset to order element
                builder.up();
            }
        }

        private static void decorateBrowser(XmlBuilder builder, ShopperBrowser browser)
        {
            if (browser != null)
            {
                builder.e("browser")
                       .e("acceptHeader").cdata(browser.AcceptHeader).up()
                       .e("userAgentHeader").cdata(browser.UserAgentHeader).up()
                        .up();
            }
        }

    }
}
