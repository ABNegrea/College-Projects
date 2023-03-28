using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs.Domain
{
    internal class Child : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Child(string fn, string ln, int a)
        {
            this.Id = Guid.NewGuid();
            this.FirstName = fn;
            this.LastName = ln;
            this.Age = a;
        }
        public Child(Guid guid, string fn, string ln, int a)
        {
            this.Id = guid;
            this.FirstName = fn;
            this.LastName = ln;
            this.Age = a;
        }

    }
}
