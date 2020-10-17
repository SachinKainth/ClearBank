using System;

namespace ClearBank.DeveloperTest.Exceptions
{
    public class PaymentSchemeNotFoundException : Exception
    {
        public PaymentSchemeNotFoundException(string message) : base(message)
        {
        }
    }
}