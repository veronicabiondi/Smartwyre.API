using System.Collections.Generic;
using System.Net;

namespace Smartwyre.DeveloperTest.Result
{
    //Result Classes can also be used in the API's for handling consistently responses for all scenarios 2xx/4xx/5xx
    public abstract class Result<T> 
    {
        public abstract T Value { get; }

        public abstract IEnumerable<ErrorMessage> Errors { get; }

        public abstract int StatusCode { get; }

        public bool IsSuccessful => StatusCode >= (int)HttpStatusCode.OK && StatusCode < (int)HttpStatusCode.Ambiguous;
    }
}