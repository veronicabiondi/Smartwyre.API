using System.Collections.Generic;
using System.Net;

namespace Smartwyre.DeveloperTest.Result
{
    public class BadRequestResult<T> : Result<T>
    {
        public BadRequestResult(IEnumerable<ErrorMessage> messages) => Errors = messages;

        public override T Value => default(T);

        public override IEnumerable<ErrorMessage> Errors { get; }

        public override int StatusCode => (int)HttpStatusCode.BadRequest;
    }
}