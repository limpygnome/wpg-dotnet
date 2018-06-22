using System;
using wpg.connection.http;

namespace wpg.exception
{
    public class WpgMalformedException : WpgException
    {
        public WpgMalformedException(HttpResponse response) : base("Unexpected response from gateway")
        {
            this.Response = response;
        }

        public WpgMalformedException(string message) : base(message)
        {
        }

        public WpgMalformedException(string message, HttpResponse response) : base(message)
        {
            this.Response = response;
        }

        public WpgMalformedException(string message, string content) : base(message)
        {
            this.Content = content;
        }

        public WpgMalformedException(string message, string content, Exception cause) : base(message, cause)
        {
            this.Content = content;
        }

        public HttpResponse Response { get; private set; }

        public string Content { get; private set; }

    }
}
