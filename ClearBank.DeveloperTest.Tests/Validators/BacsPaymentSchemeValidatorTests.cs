using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class BacsPaymentSchemeValidatorTests
    {
        [Theory]
        [InlineData(AllowedPaymentSchemes.Bacs, true)]
        [InlineData(AllowedPaymentSchemes.Chaps, false)]
        [InlineData(AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.FasterPayments, true)]
        public void IsAccountInValidState_WhenAccountAllowedPaymentSchemesSetToSpecificValue_ReturnsCorrectResponse
            (AllowedPaymentSchemes allowedPaymentSchemes, bool expectedValidity)
        {
            var sut = new BacsPaymentSchemeValidator(new Account { AllowedPaymentSchemes = allowedPaymentSchemes });

            sut.IsAccountInValidState().Should().Be(expectedValidity);
        }
    }
}
