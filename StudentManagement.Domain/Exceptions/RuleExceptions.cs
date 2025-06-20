using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Exceptions
{
    public class RuleExceptions : Exception
    {
        public RuleExceptions(string message) : base(message) { }
    }
}
