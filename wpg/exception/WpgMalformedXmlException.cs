using System;
namespace wpg.exception
{
    public class WpgMalformedXmlException : WpgException
    {
        public WpgMalformedXmlException(String text) : base("XML could not be parsed")
        {
            this.Text = text;
        }

        public WpgMalformedXmlException(String text, Exception cause) : base("XML could not be parsed", cause)
        {
            this.Text = text;
        }

        public String Text { get; private set; }

    }
}
