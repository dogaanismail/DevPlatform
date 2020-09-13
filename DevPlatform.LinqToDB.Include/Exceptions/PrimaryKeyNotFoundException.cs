using System;

namespace DevPlatform.LinqToDB.Include.Exceptions
{
    public class PrimaryKeyNotFoundException : Exception
    {
        public PrimaryKeyNotFoundException()
        {

        }

        public PrimaryKeyNotFoundException(string message) : base(message)
        {

        }
    }
}
