using System;

namespace DevPlatform.LinqToDB.Include.Exceptions
{
    public class PropertyAccessorNotFoundException : Exception
    {
        public PropertyAccessorNotFoundException()
        {

        }

        public PropertyAccessorNotFoundException(string message) : base(message)
        {

        }
    }
}
