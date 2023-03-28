using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Bonus.Domain.Exceptions
{
    internal class ElevException: Exception
    {
        public ElevException(string message) : base(message) { }
    }
}
