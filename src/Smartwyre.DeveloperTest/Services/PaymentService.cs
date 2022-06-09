using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Result;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDatabaseContext _context;
        private readonly IValidationService _validationService;

        public PaymentService(IDatabaseContext context, IValidationService validationService) => (_context, _validationService)  = (context, validationService);

        public async Task<Result<MakePaymentResult>> MakePaymentAsync(MakePaymentCommand request)
        {
            var account = _context.GetDataSet<Account>().FirstOrDefault(x => x.AccountNumber == request.DebtorAccountNumber);                                        

            if (account == null)
            {
                return new NoResult<MakePaymentResult>(new List<ErrorMessage>() { new ErrorMessage((int)Codes.ACCOUNT_NOTFOUND, ValidationFailCodes.PaymentCodes[Codes.ACCOUNT_NOTFOUND]) });
            }
            var transactionResult = (request.PaymentScheme == PaymentScheme.BankToBankTransfer) ? _validationService.ValidateBankToBankTransferPayment(account):
                         (request.PaymentScheme == PaymentScheme.ExpeditedPayments) ? _validationService.ValidateExpeditedPaymentsPayment(account, request): 
                                                                                       _validationService.ValidateAutomatedPaymentSystemPayment(account);


            if (!transactionResult.IsSuccessful)
            {
                var errors = transactionResult.Errors;
                return new FailureResult<MakePaymentResult>(errors);
            }

            account.Balance -= request.Amount;

            await _context.SaveChangesAsync();

            return new SuccessResult<MakePaymentResult>(new MakePaymentResult() { Success = true });
        }
    }
}
