using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services
{
    public class ValidationServiceTests
    {
        private readonly ValidationService _validationService;

        public ValidationServiceTests() => _validationService = new ValidationService();

        [Fact]
        public void Should_ValidateAutomatedPaymentSystemPayment_Return_Success()
        {
            //ACT
            var account = new Account()
            {
                AccountNumber = "12234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem,
                Balance = 1000m,
                Status = AccountStatus.Live
            };

            var result = _validationService.ValidateAutomatedPaymentSystemPayment(account);

            //ASSERT
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Should_ValidateAutomatedPaymentSystemPayment_Invalid_Account_State_Fail()
        {
            //ACT
            var account = new Account()
            {
                AccountNumber = "12234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem,
                Balance = 1000m,
                Status = AccountStatus.Disabled
            };

            var result = _validationService.ValidateAutomatedPaymentSystemPayment(account);

            //ASSERT
            Assert.True(result.Errors.Any());
        }

        [Fact]
        public void Should_ValidateBankToBankTransferPayment_Return_Success()
        {
            //ACT
            var account = new Account()
            {
                AccountNumber = "12234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer,
                Balance = 1000m,
                Status = AccountStatus.Live
            };

            var result = _validationService.ValidateBankToBankTransferPayment(account);

            //ASSERT
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void Should_ValidateBankToBankTransferPayment_Invalid_Scheme_Fail()
        {
            //ACT
            var account = new Account()
            {
                AccountNumber = "12234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments,
                Balance = 1000m,
                Status = AccountStatus.Live
            };

            var result = _validationService.ValidateBankToBankTransferPayment(account);

            //ASSERT
            Assert.True(!result.IsSuccessful);
            Assert.True(result.Errors.First().ErrorCode == (int)Codes.INVALID_SCHEME);
        }

        [Fact]
        public void Should_ValidateExpeditedPaymentsPayment_InsufficentFunds_State_Fail()
        {
            //ACT
            var account = new Account()
            {
                AccountNumber = "12234",
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments,
                Balance = 1000m,
                Status = AccountStatus.Disabled
            };

            var command = new MakePaymentCommand()
            {
                Amount = 1200
            };

            var result = _validationService.ValidateExpeditedPaymentsPayment(account, command);

            //ASSERT
            Assert.True(!result.IsSuccessful);
            Assert.True(result.Errors.First().ErrorCode == (int)Codes.LOW_BALANCE);
        }
    }
}
