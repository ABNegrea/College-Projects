using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain.Exceptions;

namespace Proiect_Bonus.Domain.Validators
{
    internal class EchipaValidator
    {
        public static string NumeValidator(string nume)
        {
            if (nume != null)
                return nume;
            else
                throw new EchipaException("Numele nu poate fi null!");
        }
    }
}
