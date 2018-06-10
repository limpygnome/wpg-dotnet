using System;
namespace wpg.exception
{
    public abstract class WpgException : Exception
    {
        public WpgException(String message) : base(message) { }

        public WpgException(String message, Exception cause) : base(message, cause) { }
    }
}
