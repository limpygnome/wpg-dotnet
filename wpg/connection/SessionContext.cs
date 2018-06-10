using System;
using System.Collections.Generic;
namespace wpg.connection
{
    public class SessionContext
    {
        public SessionContext()
        {
            this.SessionId = System.Guid.NewGuid().ToString();
            this.Headers = new Dictionary<string, string>();
        }

        public String SessionId { get; set; }

        public Dictionary<String, String> Headers { get; private set; }

    }
}
