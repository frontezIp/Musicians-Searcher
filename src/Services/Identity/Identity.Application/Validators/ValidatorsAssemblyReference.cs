using System.Reflection;

namespace Identity.Application.Validators
{
    public class ValidatorsAssemblyReference
    {
        public static Assembly Assembly 
            => typeof(ValidatorsAssemblyReference).Assembly;
    }
}
