using System;
using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Patterns;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Patterns
{
    public class PaymentSchemeValidatorFactoryTests
    {
        private readonly PaymentSchemeValidatorFactory _sut;

        public PaymentSchemeValidatorFactoryTests()
        {

            _sut = new PaymentSchemeValidatorFactory();
        }

        [Fact]
        public void Create_WhenPaymentSchemeInvalid_ThrowsException()
        {
            Action act = () => _sut.Create((PaymentScheme)(-1), null, null);

            act.Should().Throw<PaymentSchemeNotFoundException>()
                .WithMessage($"The payment scheme '-1' was not found.");
        }

        [Fact]
        public void Create_WhenPaymentSchemeBacs_CreatesBacsValidator()
        {
            const string debtorAccountNumber = "Bacs Account";
            var account = new Account {AccountNumber = debtorAccountNumber};
            var request = new MakePaymentRequest();

            var validator = _sut.Create(PaymentScheme.Bacs, account, request);

            validator.Should().BeOfType<BacsPaymentSchemeValidator>();
            var bacsValidator = (BacsPaymentSchemeValidator) validator;
            bacsValidator.Account.AccountNumber.Should().Be("Bacs Account");
        }

        [Fact]
        public void Create_WhenPaymentSchemeChaps_CreatesChapsValidator()
        {
            const string debtorAccountNumber = "Chaps Account";
            var account = new Account { AccountNumber = debtorAccountNumber };
            var request = new MakePaymentRequest();

            var validator = _sut.Create(PaymentScheme.Chaps, account, request);

            validator.Should().BeOfType<ChapsPaymentSchemeValidator>();
            var chapsValidator = (ChapsPaymentSchemeValidator)validator;
            chapsValidator.Account.AccountNumber.Should().Be("Chaps Account");
        }

        [Fact]
        public void Create_WhenPaymentSchemeFasterPayments_CreatesFasterPaymentsValidator()
        {
            const string debtorAccountNumber = "Faster Payments Account";
            var account = new Account { AccountNumber = debtorAccountNumber };
            var request = new MakePaymentRequest{DebtorAccountNumber = debtorAccountNumber};

            var validator = _sut.Create(PaymentScheme.FasterPayments, account, request);

            validator.Should().BeOfType<FasterPaymentsPaymentSchemeValidator>();
            var fasterPaymentsValidator = (FasterPaymentsPaymentSchemeValidator)validator;
            fasterPaymentsValidator.Account.AccountNumber.Should().Be("Faster Payments Account");
            fasterPaymentsValidator.MakePaymentRequest.DebtorAccountNumber.Should().Be("Faster Payments Account");
        }
    }
}
