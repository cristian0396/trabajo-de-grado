using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Validations.Base
{
    public interface IValidationRule<in T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }
}
