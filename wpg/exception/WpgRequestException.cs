using System;

namespace Worldpay
{
    public class WpgRequestException : Exception
    {
        public WpgRequestException(String message) : base(message) { }

        public WpgRequestException(String message, Exception cause) : base(message, cause) { }
    }
}
