using System.Collections.Generic;
using System.Net;

namespace Smartwyre.DeveloperTest.Result
{ 
    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T value) => Value = value;

        public override IEnumerable<ErrorMessage> Errors => new List<ErrorMessage>();

        public override int StatusCode => (int)HttpStatusCode.OK;

        public override T Value { get; }
    }
}