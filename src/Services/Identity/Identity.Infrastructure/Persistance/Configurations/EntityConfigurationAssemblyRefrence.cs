using System.Reflection;

namespace Identity.Infrastructure.Persistance.Configurations
{
    public class EntityConfigurationAssemblyRefrence
    {
        public static Assembly Assembly
            => typeof(EntityConfigurationAssemblyRefrence).Assembly;
    }
}
