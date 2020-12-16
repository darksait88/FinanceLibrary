using System;

namespace FinanceLibrary.Exceptions
{
    public class CbrApiException : Exception
    {
        public CbrApiException(string message)
            : base(message)
        { }
    }
    public class FinanceApiException : Exception
    {
        public FinanceApiException(string message)
            : base(message)
        { }
    }
    public class ConverterException : Exception
    {
        public ConverterException(string message)
            : base(message)
        { }
    }
}
