using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Patterns
{
    public class PaymentSchemeValidatorFactory : IPaymentSchemeValidatorFactory
    {
        public IPaymentSchemeValidator Create(PaymentScheme paymentScheme, Account account, MakePaymentRequest request)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return new BacsPaymentSchemeValidator(account);
                case PaymentScheme.Chaps:
                    return new ChapsPaymentSchemeValidator(account);
                case PaymentScheme.FasterPayments:
                    return new FasterPaymentsPaymentSchemeValidator(account, request);
                default:
                    throw new PaymentSchemeNotFoundException($"The payment scheme '{paymentScheme}' was not found.");
            }
        }
    }
}
