using System.Collections.Generic;

namespace Worldpay
{
    public class ShopperBrowser
    {
        public ShopperBrowser() { }

        public ShopperBrowser(string acceptHeader, string userAgentHeader)
        {
            this.AcceptHeader = acceptHeader;
            this.UserAgentHeader = userAgentHeader;
        }

        public ShopperBrowser(string acceptHeader, string userAgentHeader, string httpAcceptLanguageHeader, string httpReferer, string activeHeaders, string timeZone, string resolution)
        {
            this.AcceptHeader = acceptHeader;
            this.UserAgentHeader = userAgentHeader;
            this.HttpAcceptLanguageHeader = httpAcceptLanguageHeader;
            this.HttpReferer = httpReferer;
            this.ActiveHeaders = activeHeaders;
            this.TimeZone = timeZone;
            this.Resolution = resolution;
        }

        public string AcceptHeader { get; set; }
        public string UserAgentHeader { get; set; }
        public string HttpAcceptLanguageHeader { get; set; }
        public string HttpReferer { get; set; }
        public string ActiveHeaders { get; set; }
        public string TimeZone { get; set; }
        public string Resolution { get; set; }

        public override bool Equals(object obj)
        {
            var browser = obj as ShopperBrowser;
            return browser != null &&
                   AcceptHeader == browser.AcceptHeader &&
                   UserAgentHeader == browser.UserAgentHeader &&
                   HttpAcceptLanguageHeader == browser.HttpAcceptLanguageHeader &&
                   HttpReferer == browser.HttpReferer &&
                   ActiveHeaders == browser.ActiveHeaders &&
                   TimeZone == browser.TimeZone &&
                   Resolution == browser.Resolution;
        }

        public override int GetHashCode()
        {
            var hashCode = -1030314106;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AcceptHeader);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserAgentHeader);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HttpAcceptLanguageHeader);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HttpReferer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ActiveHeaders);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TimeZone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Resolution);
            return hashCode;
        }

    }
}
