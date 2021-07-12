using System;
using System.Runtime.Serialization;

namespace HelpLac.Domain.Validation
{
    public class ValidationEntityException : ArgumentException
    {
        public ValidationEntityException()
        {
        }

        public ValidationEntityException(string message) : base(message)
        {
        }

        public ValidationEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ValidationEntityException(string message, string paramName) : base(message, paramName)
        {
        }

        public ValidationEntityException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }

        protected ValidationEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
