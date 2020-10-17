using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Moq;
using System;
using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Patterns;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _sut;
        private readonly Mock<IDataStore> _dataStoreMock;
        private readonly Mock<IPaymentSchemeValidatorFactory> _paymentSchemeValidatorFactoryMock;

        public PaymentServiceTests()
        {
            _dataStoreMock = new Mock<IDataStore>();
            _paymentSchemeValidatorFactoryMock = new Mock<IPaymentSchemeValidatorFactory>();

            _sut = new PaymentService(_dataStoreMock.Object, _paymentSchemeValidatorFactoryMock.Object);
        }

        [Fact]
        public void MakePayment_WhenDebtorAccountDoesNotExist_ThrowsException()
        {
            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = "invalid account"
            };

            Action act = () => _sut.MakePayment(request);

            act.Should().Throw<DebtorAccountNotFoundException>()
                .WithMessage($"The debtor account with id 'invalid account' was not found.");
        }

        [Fact]
        public void MakePayment_WhenAccountIsInInvalidState_ReturnsUnsuccessfulResponse()
        {
            const string debtorAccountNumber = "Valid Account";

            _dataStoreMock
                .Setup(_ => _.GetAccount(It.Is<string>(s => s == debtorAccountNumber)))
                .Returns(new Account {AccountNumber = debtorAccountNumber});
            _paymentSchemeValidatorFactoryMock
                .Setup(_ => _.Create(
                    It.Is<PaymentScheme>(p => p == PaymentScheme.Bacs),
                    It.Is<Account>(a => a.AccountNumber == debtorAccountNumber),
                    It.Is<MakePaymentRequest>(
                        r => r.DebtorAccountNumber == debtorAccountNumber && r.PaymentScheme == PaymentScheme.Bacs)))
                .Returns(new PaymentSchemeValidatorStub(false));

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = debtorAccountNumber,
                PaymentScheme = PaymentScheme.Bacs
            };
            
            var result = _sut.MakePayment(request);

            result.Success.Should().Be(false);
        }

        [Fact]
        public void MakePayment_WhenAccountIsInValidState_ReturnsSuccessfulResponse()
        {
            const string debtorAccountNumber = "Valid Account";

            _dataStoreMock
                .Setup(_ => _.GetAccount(It.Is<string>(s => s == debtorAccountNumber)))
                .Returns(new Account { AccountNumber = debtorAccountNumber, Balance = 1000 });
            _paymentSchemeValidatorFactoryMock
                .Setup(_ => _.Create(
                    It.Is<PaymentScheme>(p => p == PaymentScheme.Bacs),
                    It.Is<Account>(a => a.AccountNumber == debtorAccountNumber),
                    It.Is<MakePaymentRequest>(
                        r => r.DebtorAccountNumber == debtorAccountNumber && r.PaymentScheme == PaymentScheme.Bacs)))
                .Returns(new PaymentSchemeValidatorStub(true));

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = debtorAccountNumber,
                PaymentScheme = PaymentScheme.Bacs,
                Amount = 300
            };

            var result = _sut.MakePayment(request);

            result.Success.Should().Be(true);
            _dataStoreMock.Verify(_=>_.UpdateAccount(It.Is<Account>(a=>a.Balance == 700)));
        }
    }
}
