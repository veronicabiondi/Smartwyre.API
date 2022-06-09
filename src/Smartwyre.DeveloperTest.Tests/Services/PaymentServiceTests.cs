using Microsoft.EntityFrameworkCore;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Result;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentServiceTests
    {
        private readonly Mock<IDatabaseContext> _context;
        private readonly Mock<IValidationService> _validationService;
        private readonly PaymentService _paymentService;

        public PaymentServiceTests()
        {
            _validationService = new Mock<IValidationService>();
            _context = new Mock<IDatabaseContext>();
            _paymentService = new PaymentService(_context.Object, _validationService.Object);
            InitialiseDataSet();
        }

        [Fact]
        public async Task Should_MakePaymentAsync_Return_Success()
        {
            //ARRANGE
             _validationService.Setup(x => x.ValidateAutomatedPaymentSystemPayment(It.IsAny<Account>())).Returns(new SuccessResult<bool>(true));

            //ACT
            var command = new MakePaymentCommand()
            {
                Amount = 1000,
                DebtorAccountNumber = "11223344",
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            };

            var result = await _paymentService.MakePaymentAsync(command);

            //ASSERT
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task Should_MakePaymentAsync_AllowedPaymentSchemes_Account_INVALID_State_Return_FailResult()
        {
            //ARRANGE
           
            var failResult = new FailureResult<bool>(new List<ErrorMessage>() { new ErrorMessage((int)Codes.ACCOUNT_NOT_LIVE, ValidationFailCodes.PaymentCodes[Codes.ACCOUNT_NOT_LIVE]) });
            _validationService.Setup(x => x.ValidateAutomatedPaymentSystemPayment(It.IsAny<Account>())).Returns(failResult);

            //ACT
            var command = new MakePaymentCommand()
            {
                Amount = 1000,
                DebtorAccountNumber = "11223344",
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            };

            var result = await _paymentService.MakePaymentAsync(command);

            //ASSERT
            Assert.True(!result.IsSuccessful);
            Assert.True(result.Errors.First().ErrorCode == (int)Codes.ACCOUNT_NOT_LIVE);
        }

        [Fact]
        public async Task Should_MakePaymentAsync_Account_Not_Found()
        {
            //ARRANGE
            _validationService.Setup(x => x.ValidateAutomatedPaymentSystemPayment(It.IsAny<Account>())).Returns(new SuccessResult<bool>(true));

            //ACT
            var command = new MakePaymentCommand()
            {
                Amount = 1000,
                DebtorAccountNumber = "11223344s",
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            };

            var result = await _paymentService.MakePaymentAsync(command);

            //ASSERT
            Assert.True(!result.IsSuccessful);
            Assert.True(result.Errors.First().ErrorCode == (int)Codes.ACCOUNT_NOTFOUND);
        }

        private void InitialiseDataSet()
        {
            var accounts = new List<Account>()
            {
                new Account()
                {
                   AccountNumber = "11223344",
                   Balance = 1000m,
                   AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem,
                   Status = AccountStatus.Live
                }
            };

            var queryable = accounts.AsQueryable();

            var dbSet = new Mock<DbSet<Account>>();

            dbSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);
            dbSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(queryable.Provider);

            _context.Setup(x => x.GetDataSet<Account>()).Returns(dbSet.Object);
        }
    }
}
