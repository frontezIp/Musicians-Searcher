using System.Reflection;

namespace Chat.BusinessLogic
{
    public static class BusinessLogicAssemblyReference
    {
        public static Assembly Assembly 
            => typeof(BusinessLogicAssemblyReference).Assembly;
    }
}
