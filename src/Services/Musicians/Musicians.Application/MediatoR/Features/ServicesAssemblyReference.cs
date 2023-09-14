using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Musicians.Application.MediatoR.Features
{
    public static class ServicesAssemblyReference
    {
        public static Assembly Assembly 
            => typeof(ServicesAssemblyReference).Assembly;
    }
}
