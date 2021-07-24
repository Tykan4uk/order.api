using System;

namespace OrderApi.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
