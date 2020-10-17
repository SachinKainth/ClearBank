using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class FasterPaymentsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public FasterPaymentsPaymentSchemeValidator(Account account, MakePaymentRequest request)
        {
            Account = account;
            MakePaymentRequest = request;
        }

        public Account Account { get; }
        public MakePaymentRequest MakePaymentRequest { get; }

        public bool IsAccountInValidState()
        {
            return 
                Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) &&
                Account.Balance >= MakePaymentRequest.Amount;
        }
    }
}
