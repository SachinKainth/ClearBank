using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class FasterPaymentsPaymentSchemeValidatorTests
    {
        [Theory]
        [InlineData(AllowedPaymentSchemes.FasterPayments, 1000, 300, true)]
        [InlineData(AllowedPaymentSchemes.Chaps, 1000, 300, false)]
        [InlineData(AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments, 1000, 300, true)]
        [InlineData(AllowedPaymentSchemes.FasterPayments, 300, 1000, false)]
        [InlineData(AllowedPaymentSchemes.Chaps, 300, 1000, false)]
        [InlineData(AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments, 300, 1000, false)]
        [InlineData(AllowedPaymentSchemes.FasterPayments, 1000, 1000, true)]
        [InlineData(AllowedPaymentSchemes.Chaps, 1000, 1000, false)]
        [InlineData(AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments, 1000, 1000, true)]
        public void IsAccountInValidState_WhenAccountAllowedPaymentSchemesSetToSpecificValue_ReturnsCorrectResponse
            (AllowedPaymentSchemes allowedPaymentSchemes, decimal balance, decimal paymentAmount, bool expectedValidity)
        {
            var sut = new FasterPaymentsPaymentSchemeValidator(
                new Account { AllowedPaymentSchemes = allowedPaymentSchemes, Balance = balance },
                new MakePaymentRequest { Amount = paymentAmount });

            sut.IsAccountInValidState().Should().Be(expectedValidity);
        }
    }
}
