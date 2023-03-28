using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Domain;

namespace Proiect_Bonus.Repository
{
    internal class JucatorActivRepository : AbstractRepository<Guid, JucatorActiv>
    {
        public string filename { get; set; }

        public JucatorActivRepository(string filename) : base(filename)
        {

        }
        public override void LoadData()
        {
            entities = new List<JucatorActiv>();
            string path = Directory.GetCurrentDirectory() + @"\Data\JucatoriActivi.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    JucatorActiv juc = new JucatorActiv(Guid.Parse(fields[0]), Guid.Parse(fields[1]), Guid.Parse(fields[2]), int.Parse(fields[3]),fields[4]);
                    entities.Add(juc);
                }
            }
        }
    }
}
