using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs.Domain
{
    internal class Entity<ID>
    {
        public ID Id { get; set; }
    }
}
