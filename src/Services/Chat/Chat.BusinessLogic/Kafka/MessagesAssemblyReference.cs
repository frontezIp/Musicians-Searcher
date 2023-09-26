using Shared;
using System.Reflection;

namespace Chat.BusinessLogic.Kafka
{
    public static class MessagesAssemblyReference
    {
        public static Assembly Assembly
            => typeof(SharedAssemblyReference).Assembly;
    }
}
