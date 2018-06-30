using System;

namespace Worldpay
{
    public abstract class WpgException : Exception
    {
        public WpgException(string message) : base(message) { }

        public WpgException(string message, Exception cause) : base(message, cause) { }
    }
}
