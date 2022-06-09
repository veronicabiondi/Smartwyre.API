using Smartwyre.DeveloperTest.Result;
using Smartwyre.DeveloperTest.Types;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IPaymentService
    {
        Task<Result<MakePaymentResult>> MakePaymentAsync(MakePaymentCommand request);
    }
}
