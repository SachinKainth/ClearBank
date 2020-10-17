using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Patterns
{
    public interface IPaymentSchemeValidatorFactory
    {
        IPaymentSchemeValidator Create(PaymentScheme paymentScheme, Account account, MakePaymentRequest request);
    }
}