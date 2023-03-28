using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Bonus.Domain
{
    internal class JucatorActiv: Entity<Guid>
    {
        public Guid idJucator { get; set; }
        public Guid idMeci { get; set; }
        public int puncteInscrise { get; set; }
        public string tip { get; set; }

        public JucatorActiv(Guid idJucator, Guid idMeci, int puncteInscrise, string tip)
        {
            this.id = Guid.NewGuid();
            this.idJucator = idJucator;
            this.idMeci = idMeci;
            this.puncteInscrise = puncteInscrise;
            this.tip = tip;
        }
        public JucatorActiv(Guid id, Guid idJucator, Guid idMeci, int puncteInscrise, string tip)
        {
            this.id = id;
            this.idJucator = idJucator;
            this.idMeci = idMeci;
            this.puncteInscrise = puncteInscrise;
            this.tip = tip;
        }
    }
}
