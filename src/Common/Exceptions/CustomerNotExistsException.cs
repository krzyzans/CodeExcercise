using System;

namespace CodeExcercise.Common.Exceptions
{
    public  class CustomerNotExistsException : Exception
    {
        public CustomerNotExistsException(string message)
            : base(message)
        {
            
        }
    }
}
