using System.Collections.Generic;
using System.Net;

namespace Smartwyre.DeveloperTest.Result
{
    public class FailureResult<T> : Result<T>
    {
        public FailureResult(IEnumerable<ErrorMessage> message) => Errors = message;

        public  override IEnumerable<ErrorMessage> Errors { get; }

        public override int StatusCode => (int)HttpStatusCode.InternalServerError;

        public override T Value => default;
    }
}