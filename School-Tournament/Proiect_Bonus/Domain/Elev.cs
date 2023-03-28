using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain.Validators;

namespace Proiect_Bonus.Domain
{
    internal class Elev: Entity<Guid>
    {
        public string nume { get; set; }

        public string scoala { get; set; } 


        public Elev(string nume, string scoala)
        {
            this.id = Guid.NewGuid();
            this.nume = ElevValidator.NumeValidator(nume);
            this.scoala = scoala;
        }

        public Elev(Guid id, string nume, string scoala)
        {
            this.id = id;
            this.nume = ElevValidator.NumeValidator(nume);
            this.scoala = scoala;
        }
    }
}
