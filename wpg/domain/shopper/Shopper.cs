using System.Collections.Generic;

namespace Worldpay
{
    public class Shopper
    {
        public Shopper(ShopperBrowser browser) : this(null, null, browser, null) { }

        public Shopper(string email) : this(email, null, null) { }

        public Shopper(string email, string shopperId) : this(email, null, null, shopperId) { }

        public Shopper(string email, string ipAddress, ShopperBrowser browser) : this(email, ipAddress, browser, null) { }

        public Shopper(string email, string ipAddress, ShopperBrowser browser, string shopperId)
        {
            this.Email = email;
            this.IpAddress = ipAddress;
            this.Browser = browser;
            this.ShopperId = shopperId;
        }

        public string Email { get; set; }
        public string IpAddress { get; set; }
        public ShopperBrowser Browser { get; set; }
        public string ShopperId { get; set; }

        public override bool Equals(object obj)
        {
            var shopper = obj as Shopper;
            return shopper != null &&
                   Email == shopper.Email &&
                   IpAddress == shopper.IpAddress &&
                   EqualityComparer<ShopperBrowser>.Default.Equals(Browser, shopper.Browser) &&
                   ShopperId == shopper.ShopperId;
        }

        public override int GetHashCode()
        {
            var hashCode = -990857817;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(IpAddress);
            hashCode = hashCode * -1521134295 + EqualityComparer<ShopperBrowser>.Default.GetHashCode(Browser);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShopperId);
            return hashCode;
        }

    }
}
