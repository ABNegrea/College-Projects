using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Bonus.Domain
{
    internal class Meci:Entity<Guid>
    {
        public string echipa1 { get; set; }
        public string echipa2 { get; set; }
        public DateTime data { get; set; }

        public Meci(string echipa1, string echipa2, DateTime data)
        {
            this.id = Guid.NewGuid();
            this.echipa1 = echipa1;
            this.echipa2 = echipa2;
            this.data = data;
        }

        public Meci(Guid id, string echipa1, string echipa2, DateTime data)
        {
            this.id = id;
            this.echipa1 = echipa1;
            this.echipa2 = echipa2;
            this.data = data;
        }

        public Meci(string echipa1, string echipa2)
        {
            this.id = Guid.NewGuid();
            this.echipa1 = echipa1;
            this.echipa2 = echipa2;
            this.data = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Echipa1: {echipa1} | Echipa2: {echipa2} | Data: {data.ToString("d/M/yyyy")}";
        }
    }
}
