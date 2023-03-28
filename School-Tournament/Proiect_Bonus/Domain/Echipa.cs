using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain.Validators;

namespace Proiect_Bonus.Domain
{
    internal class Echipa: Entity<Guid>
    {
        public string nume { get; set; }

        public Echipa(string nume)
        {
            this.id = Guid.NewGuid();
            this.nume= EchipaValidator.NumeValidator(nume);
        }

        public override string ToString()
        {
            return $"Nume: {nume}";
        }
    }
}
