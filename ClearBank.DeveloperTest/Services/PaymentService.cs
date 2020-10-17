using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Patterns;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStore _dataStore;
        private readonly IPaymentSchemeValidatorFactory _paymentSchemeValidatorFactory;

        public PaymentService(IDataStore dataStore, IPaymentSchemeValidatorFactory paymentSchemeValidatorFactory)
        {
            _dataStore = dataStore;
            _paymentSchemeValidatorFactory = paymentSchemeValidatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _dataStore.GetAccount(request.DebtorAccountNumber);
            if(account == null)
            {
                throw new DebtorAccountNotFoundException($"The debtor account with id '{request.DebtorAccountNumber}' was not found.");
            }

            var validator = _paymentSchemeValidatorFactory.Create(request.PaymentScheme, account, request);

            var isDebtorAccountStateValid = validator.IsAccountInValidState();
            if (isDebtorAccountStateValid)
            {
                account.Balance -= request.Amount;
                _dataStore.UpdateAccount(account);
            }

            return new MakePaymentResult
            {
                Success = isDebtorAccountStateValid
            };
        }
    }
}
