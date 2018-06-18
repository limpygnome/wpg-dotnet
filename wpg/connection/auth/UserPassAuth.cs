using System;
using System.Text.RegularExpressions;

namespace wpg.connection.auth
{
    public class UserPassAuth : Auth
    {
        public UserPassAuth(String user, String pass, String merchantCode) : this(user, pass, merchantCode, null) { }

        public UserPassAuth(String user, String pass, String merchantCode, String installationId)
        {
            if (String.IsNullOrWhiteSpace(installationId))
            {
                installationId = null;
            }

            // Validate
            if (String.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentException("User is mandatory");
            }
            else if (String.IsNullOrWhiteSpace(pass))
            {
                throw new ArgumentException("Pass is mandatory");
            }
            else if (!String.IsNullOrWhiteSpace(installationId) && !Regex.IsMatch(installationId, "^[0-9]+$"))
            {
                throw new ArgumentException("Installation (id) expected to be a numeric ID / value");
            }

            this.User = user;
            this.Pass = pass;
            this.MerchantCode = merchantCode;
            this.InstallationId = installationId;
        }

        public String User { get; set; }
        public String Pass { get; set; }
        public String MerchantCode { get; set; }
        public String InstallationId { get; set; }

    }
}
