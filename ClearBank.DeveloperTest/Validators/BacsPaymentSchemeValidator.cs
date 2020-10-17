using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public class BacsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public BacsPaymentSchemeValidator(Account account)
        {
            Account = account;
        }

        public Account Account { get; }

        public bool IsAccountInValidState()
        {
            return Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
        }
    }
}
