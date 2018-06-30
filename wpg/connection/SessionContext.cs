using System.Collections.Generic;

namespace Worldpay
{
    public class SessionContext
    {
        public SessionContext()
        {
            this.SessionId = System.Guid.NewGuid().ToString();
            this.Headers = new Dictionary<string, string>();
        }

        public string SessionId { get; set; }

        public Dictionary<string, string> Headers { get; private set; }

    }
}
