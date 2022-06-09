using Smartwyre.DeveloperTest.Result;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services
{
    public class ValidationService : IValidationService
    {
        public Result<bool> ValidateAutomatedPaymentSystemPayment(Account account) 
        {
           if (account.Status != AccountStatus.Live)
            {
                var errors = new List<ErrorMessage>
                {
                    new ErrorMessage((int)Codes.ACCOUNT_NOT_LIVE, ValidationFailCodes.PaymentCodes[Codes.ACCOUNT_NOT_LIVE])
                };

                return new FailureResult<bool>(errors);
            }


            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem))
            {
                var errors = new List<ErrorMessage>
                {
                    new ErrorMessage((int)Codes.INVALID_SCHEME, ValidationFailCodes.PaymentCodes[Codes.INVALID_SCHEME])
                };

                return new FailureResult<bool>(errors);
            }

            return new SuccessResult<bool>(true);
        }

        public Result<bool> ValidateBankToBankTransferPayment(Account account)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer))
            {
                var errors = new List<ErrorMessage>
                {
                    new ErrorMessage((int)Codes.INVALID_SCHEME, ValidationFailCodes.PaymentCodes[Codes.INVALID_SCHEME])
                };

                return new FailureResult<bool>(errors);
            }

            return new SuccessResult<bool>(true);
        }

        public Result<bool> ValidateExpeditedPaymentsPayment(Account account, MakePaymentCommand cmd) 
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments))
            {
                var errors = new List<ErrorMessage>
                {
                    new ErrorMessage((int)Codes.INVALID_SCHEME, ValidationFailCodes.PaymentCodes[Codes.INVALID_SCHEME] )
                };

                return new FailureResult<bool>(errors);
            }

            if (account.Balance < cmd.Amount)
            {
                var errors = new List<ErrorMessage>
                {
                    new ErrorMessage((int)Codes.LOW_BALANCE, ValidationFailCodes.PaymentCodes[Codes.LOW_BALANCE] )
                };
                return new FailureResult<bool>(errors);
            }

            return new SuccessResult<bool>(true);
        }
    }
}
