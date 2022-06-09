using System.Collections.Generic;
using System.Net;

namespace Smartwyre.DeveloperTest.Result
{
    public class NoResult<T> : Result<T>
    {
        public NoResult(IEnumerable<ErrorMessage> message) => Errors = message;

        public override IEnumerable<ErrorMessage> Errors { get; }

        public override T Value => default;

        public override int StatusCode => (int)HttpStatusCode.NotFound;
    }
}