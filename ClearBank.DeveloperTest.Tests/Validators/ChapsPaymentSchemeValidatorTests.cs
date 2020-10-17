using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class ChapsPaymentSchemeValidatorTests
    {
        [Theory]
        [InlineData(AllowedPaymentSchemes.Chaps, AccountStatus.Live, true)]
        [InlineData(AllowedPaymentSchemes.Bacs, AccountStatus.Live, false)]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments, AccountStatus.Live, true)]
        [InlineData(AllowedPaymentSchemes.Chaps, AccountStatus.InboundPaymentsOnly, false)]
        [InlineData(AllowedPaymentSchemes.Bacs, AccountStatus.InboundPaymentsOnly, false)]
        [InlineData(AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments, AccountStatus.InboundPaymentsOnly, false)]
        public void IsAccountInValidState_WhenAccountAllowedPaymentSchemesSetToSpecificValue_ReturnsCorrectResponse
            (AllowedPaymentSchemes allowedPaymentSchemes, AccountStatus status, bool expectedValidity)
        {
            var sut = new ChapsPaymentSchemeValidator(new Account { AllowedPaymentSchemes = allowedPaymentSchemes, Status = status });

            sut.IsAccountInValidState().Should().Be(expectedValidity);
        }
    }
}
