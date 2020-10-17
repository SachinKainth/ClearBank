using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Tests.Services
{
    internal class PaymentSchemeValidatorStub : IPaymentSchemeValidator
    {
        public bool IsValid { get; }

        public PaymentSchemeValidatorStub(bool isValid)
        {
            IsValid = isValid;
        }

        public bool IsAccountInValidState()
        {
            return IsValid;
        }
    }
}
