using Shared;
using System.Reflection;

namespace Musicians.Infrastructure.Kafka
{
    public static class MessagesAssemblyReference
    {
        public static Assembly Assembly
            => typeof(SharedAssemblyReference).Assembly;
    }
}
