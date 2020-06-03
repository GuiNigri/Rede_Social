using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Model.Exceptions
{
    public class RepositoryExceptions:Exception
    {
        public string PropertyName { get; }

        public RepositoryExceptions(string propertyName, string message):base(message)
        {
            PropertyName = propertyName;
        }

        public RepositoryExceptions(string propertyName, string message, Exception inner):base(message, inner)
        {
            PropertyName = propertyName;
        }
    }
}
