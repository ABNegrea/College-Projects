using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Bonus.Domain.Exceptions
{
    internal class JucatorException:Exception
    {
        public JucatorException(string message) : base(message) { }
    }
}
