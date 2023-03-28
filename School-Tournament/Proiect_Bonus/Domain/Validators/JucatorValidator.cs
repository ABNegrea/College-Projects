using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain.Exceptions;
using Proiect_Bonus.Domain.Validators;

namespace Proiect_Bonus.Domain.Validators
{
    internal class JucatorValidator: ElevValidator
    {
        public static List<string> LoadEchipe()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\Data\Echipe.txt";
            List<string> echipe = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    echipe.Add(line);
            }
            return echipe;
        }

        public static string EchipaValidator(string echipa)
        {
            List<string> echipe = LoadEchipe();
            foreach (string ech in echipe)
                if (ech == echipa)
                    return ech;
            throw new JucatorException("Echipa nu exista!");
        }
    }
}
