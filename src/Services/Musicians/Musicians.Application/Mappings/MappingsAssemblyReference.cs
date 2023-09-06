using System.Reflection;

namespace Musicians.Application.Mappings
{
    public class MappingsAssemblyReference
    {
        public static Assembly Assembly
            => typeof(MappingsAssemblyReference).Assembly;
    }
}
