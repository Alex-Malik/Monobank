using System;

namespace Monobank.Client
{
    public class MonobankException : Exception
    {
        public MonobankException(Exception exception) : base(null, exception)
        {
        }

        public MonobankException(string message) : base(message)
        {
        }
    }
}