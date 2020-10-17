using System;

namespace ClearBank.DeveloperTest.Exceptions
{
    public class DebtorAccountNotFoundException : Exception
    {
        public DebtorAccountNotFoundException(string message) : base(message)
        {
        }
    }
}