using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirkanAI.Domain.Utils
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class FunctionAttribute : Attribute
    {
        public string Name { get; }

        public FunctionAttribute(string name)
        {
            Name = name;
        }
    }
}
