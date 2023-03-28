using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs.Domain
{
    internal class User : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Office { get; set; }

        public User(string fn, string ln, string e, string p, int of)
        {
            this.Id = Guid.NewGuid();
            this.FirstName = fn;
            this.LastName = ln;
            this.Email = e;
            this.Password = p;
            this.Office = of;
        }
        public User(Guid guid, string fn, string ln, string e, string p, int of)
        {
            this.Id = guid;
            this.FirstName = fn;
            this.LastName = ln;
            this.Email = e;
            this.Password = p;
            this.Office = of;
        }
    }
}
