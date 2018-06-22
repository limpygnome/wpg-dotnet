using System;
using System.Collections.Generic;

namespace wpg.domain
{
    public class ShopperBrowser
    {
        public ShopperBrowser() { }

        public ShopperBrowser(String acceptHeader, String userAgentHeader)
        {
            this.AcceptHeader = acceptHeader;
            this.UserAgentHeader = userAgentHeader;
        }

        public ShopperBrowser(String acceptHeader, String userAgentHeader, String httpAcceptLanguageHeader, String httpReferer, String activeHeaders, String timeZone, String resolution)
        {
            this.AcceptHeader = acceptHeader;
            this.UserAgentHeader = userAgentHeader;
            this.HttpAcceptLanguageHeader = httpAcceptLanguageHeader;
            this.HttpReferer = httpReferer;
            this.ActiveHeaders = activeHeaders;
            this.TimeZone = timeZone;
            this.Resolution = resolution;
        }

        public String AcceptHeader { get; set; }
        public String UserAgentHeader { get; set; }
        public String HttpAcceptLanguageHeader { get; set; }
        public String HttpReferer { get; set; }
        public String ActiveHeaders { get; set; }
        public String TimeZone { get; set; }
        public String Resolution { get; set; }

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
