using System;
namespace wpg.domain
{
    public class Shopper
    {
        public Shopper(ShopperBrowser browser) : this(null, null, browser, null) { }

        public Shopper(String email) : this(email, null, null) { }

        public Shopper(String email, String shopperId) : this(email, null, null, shopperId) { }

        public Shopper(String email, String ipAddress, ShopperBrowser browser) : this(email, ipAddress, browser, null) { }

        public Shopper(String email, String ipAddress, ShopperBrowser browser, String shopperId)
        {
            this.Email = email;
            this.IpAddress = ipAddress;
            this.Browser = browser;
            this.ShopperId = shopperId;
        }

        public String Email { get; set; }
        public String IpAddress { get; set; }
        public ShopperBrowser Browser { get; set; }
        public String ShopperId { get; set; }

    }
}
