using Smartwyre.DeveloperTest.Result;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    //Validation Service can be more complicated as this is where we are most likely to encounter Business invariant rules for the Domain. Payment service does not have intelligence, the most important is the Validation rules.
    //We could have strategies for more rules etc but for now I added them as little functions. No switch cases for business rules. Switch cases hide functionality and they can't be unit tested.
    public interface IValidationService
    {
        public Result<bool> ValidateBankToBankTransferPayment(Account data);

        public Result<bool> ValidateExpeditedPaymentsPayment(Account data, MakePaymentCommand cmd);

        public Result<bool> ValidateAutomatedPaymentSystemPayment(Account data);
    }
}
