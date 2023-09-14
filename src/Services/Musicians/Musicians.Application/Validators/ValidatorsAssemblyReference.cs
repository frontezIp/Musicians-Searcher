using System.Reflection;

namespace Musicians.Application.Validators
{
    public static class ValidatorsAssemblyReference
    {
        public static Assembly Assembly
            => typeof(ValidatorsAssemblyReference).Assembly;
    }
}
