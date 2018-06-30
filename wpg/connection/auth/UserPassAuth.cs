using System;
using System.Text.RegularExpressions;

namespace Worldpay
{
    public class UserPassAuth : Auth
    {
        public UserPassAuth(string user, string pass, string merchantCode) : this(user, pass, merchantCode, null) { }

        public UserPassAuth(string user, string pass, string merchantCode, string installationId)
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

        public string User { get; set; }
        public string Pass { get; set; }
        public string MerchantCode { get; set; }
        public string InstallationId { get; set; }

    }
}
