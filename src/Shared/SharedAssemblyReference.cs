using System.Reflection;

namespace Shared
{
    public class SharedAssemblyReference
    {
        public static Assembly Assembly
            => typeof(SharedAssemblyReference).Assembly;
    }
}
