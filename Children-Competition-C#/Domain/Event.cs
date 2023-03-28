using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs.Domain
{
    internal class Event : Entity<Guid>
    {
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public int Enrolled { get; set; }

        public Event(Guid guid, string n, int min, int max, int enrolled)
        {
            this.Id = guid;
            this.Name = n;
            this.MinAge = min;
            this.MaxAge = max;
            this.Enrolled = enrolled;
        }

        public Event(string n, int min, int max, int enrolled)
        {
            this.Id = Guid.NewGuid();
            this.Name = n;
            this.MinAge = min;
            this.MaxAge = max;
            this.Enrolled = enrolled;
        }

        public string AgeToString()
        {
            return this.MinAge.ToString() + "-" + this.MaxAge.ToString();
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
