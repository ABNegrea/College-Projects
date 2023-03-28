using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs.Domain
{
    internal class Participation : Entity<Guid>
    {
        public Child Child { get; set; }
        public Event Event { get; set; }

        public Participation(Child ch, Event ev)
        {
            this.Id = Guid.NewGuid();
            this.Child = ch;
            this.Event = ev;
        }
        public Participation(Guid guid, Child ch, Event ev)
        {
            this.Id = guid;
            this.Child = ch;
            this.Event = ev;
        }
    }
}
