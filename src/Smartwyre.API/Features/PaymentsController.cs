using Microsoft.AspNetCore.Mvc;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.API.Features
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService) => _paymentService = paymentService;

        [HttpPost]
        public async Task<IActionResult> Post(MakePaymentCommand cmd)
        {
            var result = await _paymentService.MakePaymentAsync(cmd);
            return new ObjectResult(result);
        }
    }
}
