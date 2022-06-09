using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services
{
    public class ValidationFailCodes
    {
        public static Dictionary<Codes, string> PaymentCodes => new Dictionary<Codes, string>()
        {
            { Codes.ACCOUNT_NOT_LIVE, "Account State is not LIVE" },
            { Codes.LOW_BALANCE, "Insufficient funds" },
            { Codes.INVALID_SCHEME, "Invalid Scheme" },
            { Codes.ACCOUNT_NOTFOUND, "Unable to find the account" }
        };
    }

    public enum Codes
    { 
        ACCOUNT_NOT_LIVE = 01,
        LOW_BALANCE = 02,
        INVALID_SCHEME = 03,
        ACCOUNT_NOTFOUND
    }
}
