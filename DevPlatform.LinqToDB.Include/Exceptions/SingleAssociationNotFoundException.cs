using System;

namespace DevPlatform.LinqToDB.Include.Exceptions
{
    public class SingleAssociationNotFoundException : Exception
    {
        private const string DEFAULT_MESSAGE = "Single Association Not Found";
        public SingleAssociationNotFoundException() : base(DEFAULT_MESSAGE)
        {

        }

        public SingleAssociationNotFoundException(string message) : base(message)
        {

        }

        public SingleAssociationNotFoundException(Exception innerException, string message = DEFAULT_MESSAGE)
            : base(message, innerException)
        {

        }
    }
}
