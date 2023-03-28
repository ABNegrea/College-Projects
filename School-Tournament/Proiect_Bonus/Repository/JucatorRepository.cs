using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Bonus.Repository;
using Proiect_Bonus.Domain;

namespace Proiect_Bonus.Repository
{
    internal class JucatorRepository : AbstractRepository<Guid, Jucator>
    {
        public string filename { get; set; }

        public JucatorRepository(string filename) : base(filename)
        {

        }
        public override void LoadData()
        {
            entities = new List<Jucator>();
            string path = Directory.GetCurrentDirectory() + @"\Data\Jucatori.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    Jucator juc = new Jucator(Guid.Parse(fields[0]), fields[1], fields[2], fields[3]);
                    entities.Add(juc);
                }
            }
        }
    }
}