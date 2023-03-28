using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain;

namespace Proiect_Bonus.Repository
{
    internal class MeciRepository : AbstractRepository<Guid, Meci>
    {
        public string filename { get; set; }

        public MeciRepository(string filename) : base(filename)
        {

        }
        public override void LoadData()
        {
            entities = new List<Meci>();
            string path = Directory.GetCurrentDirectory() + @"\Data\Meciuri.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    Meci meci = new Meci(Guid.Parse(fields[0]), fields[1], fields[2], DateTime.ParseExact(fields[3], "d/M/yyyy", new CultureInfo("en-CA")));
                    entities.Add(meci);
                }
            }
        }
    }
}
