using System;
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

    }
}
