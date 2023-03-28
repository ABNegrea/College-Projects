using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain.Exceptions;

namespace Proiect_Bonus.Domain.Validators
{
    internal class ElevValidator
    {

        public static List<string> LoadScoli()
        {
            List<string> scoli = new List<string>();
            string path  = System.IO.Directory.GetCurrentDirectory() + @"\Data\Scoli.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    scoli.Add(line);
            }
            return scoli;
        }

        public static string ScoalaValidator(string scoala)
        {
            List<string> scoli = LoadScoli();
            foreach (string sc in scoli)
                if (sc == scoala)
                    return sc;
            throw new ElevException("Scoala nu exista!");
        }
        public static string NumeValidator(string nume)
        {
            if (nume != null)
                return nume;
            else
                throw new ElevException("Numele nu poate fi null!");
        }
    }
}
