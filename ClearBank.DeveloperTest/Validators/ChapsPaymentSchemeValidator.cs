using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class ChapsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public ChapsPaymentSchemeValidator(Account account)
        {
            Account = account;
        }

        public Account Account { get; }

        public bool IsAccountInValidState()
        {
            return 
                Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && 
                Account.Status == AccountStatus.Live;
        }
    }
}
