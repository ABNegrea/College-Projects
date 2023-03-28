using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain.Validators;

namespace Proiect_Bonus.Domain
{
    internal class Jucator: Elev
    {
        public string echipa { get; set; }

        public Jucator(string nume, string scoala, string echipa):base(nume, scoala)
        {
            this.echipa = echipa;
        }

        public Jucator(Guid id, string nume, string scoala, string echipa) : base(id, nume, scoala)
        {
            this.echipa = echipa;
        }

        public override string ToString()
        {
            return $"Nume: {nume} | Scoala: {scoala} | Echipa: {echipa}";
        }
    }
}
