using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Model.Exceptions
{
    public class ModelValidationExceptions:Exception
    {
        public string PropertyName { get; }

        public ModelValidationExceptions(string propertyName, string message):base(message)
        {
            PropertyName = propertyName;
        }
        public ModelValidationExceptions(string propertyName, string message, Exception inner):base(message, inner)
        {
            PropertyName = propertyName;
        }
    }
}
